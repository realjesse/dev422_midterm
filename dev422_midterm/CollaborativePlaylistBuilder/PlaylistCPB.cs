using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    public class PlaylistCPB : Form
    {
        private readonly PlaylistApiClient api;
        private readonly int playlistId;
        private readonly Label heading = new Label { AutoSize = true };
        private readonly DataGridView songsGrid = new DataGridView();
        private readonly Button addSongButton = new Button { Text = "Add song", AutoSize = true, Enabled = false };
        private readonly Button voteButton = new Button { Text = "Vote for selected song", AutoSize = true, Enabled = false };
        private readonly TextBox usernameTextBox = new TextBox { Width = 160 };
        private readonly Button inviteButton = new Button { Text = "Invite collaborator", AutoSize = true, Enabled = false };
        private readonly Label status = new Label { Dock = DockStyle.Fill };
        private readonly IContainer components = new Container();
        private readonly Timer timer;
        private PlaylistSummary playlist;
        private bool busy;
        private bool closed;
        private bool errorShown;

        public PlaylistCPB()
        {
            Text = "Playlist";
            ClientSize = new Size(900, 520);
            MinimumSize = new Size(700, 450);
            StartPosition = FormStartPosition.CenterParent;
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 5 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.Controls.Add(heading, 0, 0);
            songsGrid.Dock = DockStyle.Fill;
            songsGrid.ReadOnly = true;
            songsGrid.AllowUserToAddRows = false;
            songsGrid.AllowUserToDeleteRows = false;
            songsGrid.MultiSelect = false;
            songsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            songsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            layout.Controls.Add(songsGrid, 0, 1);
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill };
            var back = new Button { Text = "Back", AutoSize = true };
            back.Click += (sender, args) => Close();
            actions.Controls.Add(addSongButton);
            actions.Controls.Add(voteButton);
            actions.Controls.Add(back);
            layout.Controls.Add(actions, 0, 2);
            var invitations = new FlowLayoutPanel { Dock = DockStyle.Fill };
            invitations.Controls.Add(new Label { Text = "Username", AutoSize = true });
            invitations.Controls.Add(usernameTextBox);
            invitations.Controls.Add(inviteButton);
            layout.Controls.Add(invitations, 0, 3);
            layout.Controls.Add(status, 0, 4);
            Controls.Add(layout);

            timer = new Timer(components) { Interval = 5000 };
            timer.Tick += async (sender, args) => await RunAsync(null, true);
            Shown += async (sender, args) => { timer.Start(); await RunAsync(null); };
            FormClosed += (sender, args) => { closed = true; timer.Stop(); };
            songsGrid.SelectionChanged += (sender, args) => UpdateButtons();
            addSongButton.Click += OpenSongLibrary;
            voteButton.Click += async (sender, args) =>
            {
                if (songsGrid.CurrentRow == null) return;
                var songId = (int)songsGrid.CurrentRow.Cells["Id"].Value;
                await RunAsync(() => api.VoteAsync(playlistId, songId));
            };
            inviteButton.Click += async (sender, args) =>
            {
                var username = usernameTextBox.Text.Trim();
                if (username.Length == 0) { MessageBox.Show(this, "Enter a registered username."); return; }
                await RunAsync(async () =>
                {
                    await api.InviteCollaboratorAsync(playlistId, username);
                    if (!closed) usernameTextBox.Clear();
                });
            };
        }

        internal PlaylistCPB(PlaylistApiClient api, int playlistId) : this()
        {
            this.api = api;
            this.playlistId = playlistId;
        }

        private void UpdateButtons()
        {
            var userId = api?.CurrentUser?.Id;
            var owner = playlist != null && userId.HasValue && playlist.CreatedBy == userId.Value;
            addSongButton.Enabled = !busy && playlist != null && (owner ||
                (playlist.IsCollaborative && userId.HasValue && playlist.Collaborators.Contains(userId.Value)));
            inviteButton.Enabled = !busy && owner && playlist.IsCollaborative;
            usernameTextBox.Enabled = inviteButton.Enabled;
            voteButton.Enabled = !busy && playlist != null && songsGrid.CurrentRow != null;
        }

        private async Task RefreshPlaylistAsync()
        {
            var details = await api.GetPlaylistAsync(playlistId);
            var songs = await api.GetSongsAsync();
            if (closed) return;
            var selectedId = songsGrid.CurrentRow?.Cells["Id"].Value;
            playlist = details;
            heading.Text = details.Name + (details.IsCollaborative ? " (Collaborative)" : " (Owner edits only)");
            Text = details.Name;
            var byId = songs.ToDictionary(song => song.Id);
            songsGrid.DataSource = details.Songs.Where(byId.ContainsKey).Select(id => new
            {
                Id = id,
                byId[id].Title,
                byId[id].Artist,
                byId[id].Genre,
                byId[id].Duration,
                Votes = details.Votes != null && details.Votes.ContainsKey(id.ToString()) ? details.Votes[id.ToString()] : 0
            }).ToList();
            foreach (DataGridViewRow row in songsGrid.Rows)
                if (Equals(row.Cells["Id"].Value, selectedId)) songsGrid.CurrentCell = row.Cells["Title"];
            status.Text = "Ranked by votes. One vote per user per song, repeat votes do not add another vote.";
        }

        private async Task RunAsync(Func<Task> action, bool automatic = false)
        {
            if (busy || closed || api == null) return;
            busy = true;
            UpdateButtons();
            var completed = false;
            try
            {
                if (action != null) { await action(); completed = true; }
                if (!closed) await RefreshPlaylistAsync();
                errorShown = false;
            }
            catch (Exception exception) when (exception is HttpRequestException || exception is TaskCanceledException ||
                exception is InvalidOperationException || exception is UnauthorizedAccessException || exception is ArgumentException)
            {
                if (closed) return;
                var message = exception is HttpRequestException ? "Cannot reach the playlist API."
                    : exception is TaskCanceledException ? "The request timed out." : exception.Message;
                if (completed) message = "Change saved, but the playlist could not reload.\n\n" + message;
                status.Text = message;
                if (!automatic || !errorShown || exception is UnauthorizedAccessException) MessageBox.Show(this, message);
                errorShown = true;
                if (exception is UnauthorizedAccessException) { DialogResult = DialogResult.Retry; Close(); }
            }
            finally { busy = false; if (!closed) UpdateButtons(); }
        }

        private async void OpenSongLibrary(object sender, EventArgs e)
        {
            if (busy || playlist == null) return;
            timer.Stop();
            using (var library = new SongLibaryCPB(api, playlistId))
            {
                if (library.ShowDialog(this) == DialogResult.Retry && !closed)
                {
                    DialogResult = DialogResult.Retry;
                    Close();
                }
            }
            if (!closed) { await RunAsync(null); if (!closed) timer.Start(); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
