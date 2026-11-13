using System;
using System.Drawing;
using System.Windows.Forms;
using dev422_midterm.album_inventory_microservice;

namespace dev422_midterm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
