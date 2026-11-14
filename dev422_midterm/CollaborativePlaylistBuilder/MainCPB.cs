using System.Windows.Forms;

using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    public partial class MainCPB : Form
    {
        private readonly PlaylistApiClient api;
        private readonly Timer refreshTimer;
        private bool refreshing;
        private bool creating;
        private bool closed;
        private bool refreshErrorShown;

        public MainCPB()
        {
            InitializeComponent();
            // create a container to hold timer (makes it easier to dispose of)
            if (components == null) components = new Container();
            refreshTimer = new Timer(components) { Interval = 5000 };
            refreshTimer.Tick += async (sender, args) => await RefreshPlaylistsAsync();
            // Refresh playlist on load (don't have to wait 5 seconds)
            Shown += async (sender, args) =>
            {
                if (api == null) return;
                refreshTimer.Start();
                await RefreshPlaylistsAsync();
            };
            FormClosed += (sender, args) => { closed = true; refreshTimer.Stop(); };
            createPlaylistButton.Click += async (sender, args) => await CreatePlaylistAsync();
            signOutButton.Click += (sender, args) => { DialogResult = DialogResult.Retry; Close(); };
            openPlaylistButton.Click += OpenPlaylist;
            songLibraryButton.Click += OpenSongLibrary;
            playlistDataGridView.ReadOnly = true;
            playlistDataGridView.AllowUserToAddRows = false;
            playlistDataGridView.AllowUserToDeleteRows = false;
        }

        internal MainCPB(PlaylistApiClient api) : this()
        {
            this.api = api;
        }

        private async void OpenPlaylist(object sender, EventArgs e)
        {
            if (api == null || closed) return;
            if (playlistDataGridView.CurrentRow == null) { MessageBox.Show(this, "Select a playlist first."); return; }
            var playlistId = (int)playlistDataGridView.CurrentRow.Cells["Id"].Value;
            refreshTimer.Stop();
            try
            {
                using (var playlist = new PlaylistCPB(api, playlistId))
                {
                    if (playlist.ShowDialog(this) == DialogResult.Retry && !closed)
                    {
                        DialogResult = DialogResult.Retry;
                        Close();
                    }
                }
                if (!closed) await RefreshPlaylistsAsync();
            }
            finally { if (!closed) refreshTimer.Start(); }
        }

        private void OpenSongLibrary(object sender, EventArgs e)
        {
            if (api == null) return;
            refreshTimer.Stop();
            try
            {
                using (var library = new SongLibaryCPB(api))
                {
                    if (library.ShowDialog(this) == DialogResult.Retry && !closed)
                    {
                        DialogResult = DialogResult.Retry;
                        Close();
                    }
                }
            }
            finally { if (!closed) refreshTimer.Start(); }
        }

        private async Task RefreshPlaylistsAsync()
        {
            if (refreshing || closed || api == null) return;
            refreshing = true;
            try
            {
                var selectedId = playlistDataGridView.CurrentRow?.Cells["Id"].Value;
                var playlists = await api.GetPlaylistsAsync();
                if (closed) return;
                playlistDataGridView.DataSource = playlists.Select(playlist => new
                {
                    playlist.Id,
                    playlist.Name,
                    playlist.IsCollaborative,
                    SongCount = playlist.Songs == null ? 0 : playlist.Songs.Count
                }).ToList();
                if (selectedId != null)
                {
                    foreach (DataGridViewRow row in playlistDataGridView.Rows)
                    {
                        if (Equals(row.Cells["Id"].Value, selectedId))
                        {
                            playlistDataGridView.CurrentCell = row.Cells["Name"];
                            break;
                        }
                    }
                }
                refreshErrorShown = false;
            }
            catch (Exception exception) when (IsApiError(exception))
            {
                if (closed) return;
                if (exception is UnauthorizedAccessException) HandleError(exception);
                else if (!refreshErrorShown)
                {
                    refreshErrorShown = true;
                    HandleError(exception);
                }
            }
            finally { refreshing = false; }
        }

        private async Task CreatePlaylistAsync()
        {
            if (creating || closed || api == null) return;
            var name = playlistNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(this, "Enter a playlist name.");
                return;
            }
            creating = true;
            createPlaylistButton.Enabled = false;
            try
            {
                await api.CreatePlaylistAsync(name, isCollaborativeCheckBox.Checked);
                if (closed) return;
                playlistNameTextBox.Clear();
                isCollaborativeCheckBox.Checked = false;
                await RefreshPlaylistsAsync();
            }
            catch (Exception exception) when (IsApiError(exception))
            {
                if (!closed) HandleError(exception);
            }
            finally
            {
                creating = false;
                if (!closed) createPlaylistButton.Enabled = true;
            }
        }

        private static bool IsApiError(Exception exception)
        {
            return exception is HttpRequestException || exception is TaskCanceledException ||
                exception is InvalidOperationException || exception is UnauthorizedAccessException ||
                exception is ArgumentException;
        }

        private void HandleError(Exception exception)
        {
            var message = exception is HttpRequestException ? "Cannot reach the playlist API. Make sure it is running."
                : exception is TaskCanceledException ? "The playlist request timed out."
                : exception is ArgumentException ? "The API returned an unexpected response."
                : exception.Message;
            MessageBox.Show(this, message);
            if (exception is UnauthorizedAccessException)
            {
                DialogResult = DialogResult.Retry;
                Close();
            }
        }
    }
}
