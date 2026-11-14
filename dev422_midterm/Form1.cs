using System;
using System.Drawing;
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
            collaborativePlaylistBuilderButton.Click += OpenPlaylistBuilder;
        }

        
        // old form1 info from local branch
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

            
            // new form1 info from main merge
            Text = "VibeHive";
            ClientSize = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            Button btnAlbums = new Button
            {
                Text = "Album Inventory",
                Location = new Point(30, 30),
                Size = new Size(200, 50)
            };

            btnAlbums.Click += (sender, e) =>
            {
                using (AlbumInventoryForm form = new AlbumInventoryForm())
                {
                    form.ShowDialog(this);
                }
            };

            Controls.Add(btnAlbums);
        }
    }
}
