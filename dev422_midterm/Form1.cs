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
