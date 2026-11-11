using System;
using System.Windows.Forms;
using dev422_midterm.CollaborativePlaylistBuilder;

namespace dev422_midterm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            collaborativePlaylistBuilderButton.Click += OpenPlaylistBuilder;
        }

        private void OpenPlaylistBuilder(object sender, EventArgs e)
        {
            Hide();
            try
            {
                using (var api = new PlaylistApiClient())
                using (var login = new LoginCPB(api))
                {
                    if (login.ShowDialog() != DialogResult.OK) return;

                    // Replace this blank form with the playlist builder main form when ready.
                    // Pass it the same api instance to keep using the login cookie.
                    using (var main = new Form { Text = "Collaborative Playlist Builder" })
                    {
                        main.ShowDialog();
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
