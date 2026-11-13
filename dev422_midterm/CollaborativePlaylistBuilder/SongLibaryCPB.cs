using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    public class SongLibaryCPB : Form
    {
        private readonly PlaylistApiClient api;
        private readonly DataGridView songDataGridView = new DataGridView();
        private readonly TextBox titleTextBox = new TextBox();
        private readonly TextBox artistTextBox = new TextBox();
        private readonly TextBox genreTextBox = new TextBox();
        private readonly TextBox durationTextBox = new TextBox();
        private readonly GroupBox createSongGroupBox = new GroupBox();
        private readonly Label statusLabel = new Label();
        private bool busy;
        private bool closed;

        public SongLibaryCPB()
        {
            Text = "Song Library";
            ClientSize = new Size(850, 560);
            MinimumSize = new Size(650, 500);
            StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 4 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 190));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            songDataGridView.Dock = DockStyle.Fill;
            songDataGridView.ReadOnly = true;
            songDataGridView.AllowUserToAddRows = false;
            songDataGridView.AllowUserToDeleteRows = false;
            songDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            songDataGridView.MultiSelect = false;
            songDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            layout.Controls.Add(songDataGridView, 0, 0);

            createSongGroupBox.Text = "Create song";
            createSongGroupBox.Dock = DockStyle.Fill;
            var fields = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, Padding = new Padding(8) };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            AddField(fields, "Title", titleTextBox, 0);
            AddField(fields, "Artist", artistTextBox, 1);
            AddField(fields, "Genre", genreTextBox, 2);
            AddField(fields, "Duration (e.g. 3:45)", durationTextBox, 3);
            var createButton = new Button { Text = "Create song", AutoSize = true };
            createButton.Click += async (sender, args) => await CreateSongAsync();
            fields.Controls.Add(createButton, 1, 4);
            createSongGroupBox.Controls.Add(fields);
            layout.Controls.Add(createSongGroupBox, 0, 1);

            statusLabel.Dock = DockStyle.Fill;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            layout.Controls.Add(statusLabel, 0, 2);
            var backButton = new Button { Text = "Back", AutoSize = true };
            backButton.Click += (sender, args) => Close();
            layout.Controls.Add(backButton, 0, 3);
            Controls.Add(layout);

            Shown += async (sender, args) => await LoadSongsAsync();
            FormClosed += (sender, args) => closed = true;
        }

        internal SongLibaryCPB(PlaylistApiClient api) : this()
        {
            this.api = api;
        }

        private static void AddField(TableLayoutPanel fields, string label, TextBox input, int row)
        {
            fields.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
            input.Dock = DockStyle.Fill;
            fields.Controls.Add(input, 1, row);
        }

        private async Task RefreshSongsAsync()
        {
            var songs = await api.GetSongsAsync();
            if (closed) return;
            songDataGridView.DataSource = songs;
            statusLabel.Text = songs.Count + " song(s)";
        }

        private async Task LoadSongsAsync()
        {
            if (busy || api == null) return;
            busy = true;
            createSongGroupBox.Enabled = false;
            statusLabel.Text = "Loading songs...";
            try { await RefreshSongsAsync(); }
            catch (Exception exception) when (IsApiError(exception)) { HandleError(exception); }
            finally
            {
                busy = false;
                if (!closed) createSongGroupBox.Enabled = true;
            }
        }

        private async Task CreateSongAsync()
        {
            if (busy || closed || api == null) return;
            var title = titleTextBox.Text.Trim();
            var artist = artistTextBox.Text.Trim();
            var genre = genreTextBox.Text.Trim();
            var duration = durationTextBox.Text.Trim();
            if (title.Length == 0 || artist.Length == 0 || genre.Length == 0 || duration.Length == 0)
            {
                MessageBox.Show(this, "Enter a title, artist, genre, and duration.");
                return;
            }
            busy = true;
            createSongGroupBox.Enabled = false;
            var created = false;
            try
            {
                await api.CreateSongAsync(title, artist, genre, duration);
                created = true;
                if (closed) return;
                titleTextBox.Clear();
                artistTextBox.Clear();
                genreTextBox.Clear();
                durationTextBox.Clear();
                await RefreshSongsAsync();
            }
            catch (Exception exception) when (IsApiError(exception))
            {
                HandleError(exception, created);
            }
            finally
            {
                busy = false;
                if (!closed) createSongGroupBox.Enabled = true;
            }
        }

        private static bool IsApiError(Exception exception)
        {
            return exception is HttpRequestException || exception is TaskCanceledException ||
                exception is InvalidOperationException || exception is UnauthorizedAccessException || exception is ArgumentException;
        }

        private void HandleError(Exception exception, bool created = false)
        {
            if (closed) return;
            var message = exception is HttpRequestException ? "Cannot reach the playlist API. Make sure it is running."
                : exception is TaskCanceledException ? "The song request timed out."
                : exception is ArgumentException ? "The API returned an unexpected response."
                : exception.Message;
            if (created) message = "Song created, but the grid could not reload. Reopen the song library to load it.\n\n" + message;
            statusLabel.Text = created ? "Song created; reopen the library to reload songs." : "Request failed.";
            MessageBox.Show(this, message);
            if (exception is UnauthorizedAccessException)
            {
                DialogResult = DialogResult.Retry;
                Close();
            }
        }
    }
}
