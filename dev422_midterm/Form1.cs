using System;
using System.Windows.Forms;
using dev422_midterm.CollaborativePlaylistBuilder;
using dev422_midterm.album_inventory_microservice;

namespace dev422_midterm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "VibeHive";
            StartPosition = FormStartPosition.CenterScreen;
            collaborativePlaylistBuilderButton.Click += OpenPlaylistBuilder;
            albumInventoryMicroserviceButton.Click += (sender, e) =>
            {
                using (var form = new AlbumInventoryForm())
                {
                    form.ShowDialog(this);
                }
            };
        }

        private void OpenPlaylistBuilder(object sender, EventArgs e)
        {
            Hide();
            try
            {
                while (true)
                {
                    using (var api = new PlaylistApiClient())
                    using (var login = new LoginCPB(api))
                    {
                        if (login.ShowDialog() != DialogResult.OK) return;
                        using (var main = new MainCPB(api))
                        {
                            // Sign out or session expiry starts a fresh login session.
                            if (main.ShowDialog() != DialogResult.Retry) return;
                        }
                    }
                }
            }
            finally
            {
                Show();
            }
        }
    }
}
