using System;
using System.Windows.Forms;
using dev422_midterm.CollaborativePlaylistBuilder;
using dev422_midterm.album_inventory_microservice;
using dev422_midterm.music_rental_management;

namespace dev422_midterm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "VibeHive";
            StartPosition = FormStartPosition.CenterScreen;

            // Opens the Collaborative Playlist Builder
            collaborativePlaylistBuilderButton.Click += OpenPlaylistBuilder;

            // Opens the Album Inventory Microservice
            albumInventoryMicroserviceButton.Click += (sender, e) =>
            {
                using (var form = new AlbumInventoryForm())
                {
                    form.ShowDialog(this);
                }
            };

            // Opens the Music Rental Manager
            musicRentalManagerButton.Click += (sender, e) =>
            {
                using (var form = new MusicRentalForm())
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
                            // Sign out or session expiry starts a fresh login session
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
