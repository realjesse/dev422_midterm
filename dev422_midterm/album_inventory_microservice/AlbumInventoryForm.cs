using System.Threading;
using Grpc.Core;
using AlbumInventoryMicroserviceAPI;
using Grpc.Net.Client;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace dev422_midterm.album_inventory_microservice
{
    public partial class AlbumInventoryForm : Form
    {

        private GrpcChannel channel;
        private AlbumInventory.AlbumInventoryClient client;
        private CancellationTokenSource watchCancellation =
    new CancellationTokenSource();
        private TextBox txtId;
        private TextBox txtTitle;
        private TextBox txtArtist;
        private TextBox txtGenre;
        private TextBox txtYear;
        private CheckBox chkAvailable;

        private Button btnAdd;
        private Button btnFind;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;

        private DataGridView dgvAlbums;

        public AlbumInventoryForm()
        {
            InitializeComponent();
            BuildScreen();

            // Set the server address.
            channel = GrpcChannel.ForAddress(
                "https://localhost:7112",
                new GrpcChannelOptions
                {
                    HttpHandler = new WinHttpHandler()
                });

            client = new AlbumInventory.AlbumInventoryClient(channel);

            btnRefresh.Click += RefreshAlbums;
            btnAdd.Click += AddAlbum;
            btnFind.Click += FindAlbum;
            btnUpdate.Click += UpdateAlbum;
            btnDelete.Click += DeleteAlbum;

            // Release resources when the window closes.
            Shown += WatchAlbums;

            // Stop automatic updates when closing.
            FormClosed += (sender, e) =>
            {
                watchCancellation.Cancel();
                channel.Dispose();
            };
        }

        // Load available albums from the server.
        private async void RefreshAlbums(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;

            try
            {
                List<Album> albums = new List<Album>();

                using (var call = client.ListAlbums(
                    new ListAlbumsRequest(),
                    deadline: DateTime.UtcNow.AddSeconds(10)))
                {
                    while (await call.ResponseStream.MoveNext(
                        CancellationToken.None))
                    {
                        albums.Add(call.ResponseStream.Current);
                    }
                }

                dgvAlbums.DataSource = albums;
            }
            catch (RpcException ex)
            {
                MessageBox.Show(
                    "Could not load albums: " + ex.Status.Detail);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        // Validate and add a new album.
        private async void AddAlbum(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtArtist.Text) ||
                string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Enter a title, artist, and genre.");
                return;
            }

            int year;

            if (!int.TryParse(txtYear.Text, out year) || year <= 0)
            {
                MessageBox.Show("Enter a valid year.");
                return;
            }

            btnAdd.Enabled = false;

            try
            {
                Album album = new Album
                {
                    Title = txtTitle.Text.Trim(),
                    Artist = txtArtist.Text.Trim(),
                    Genre = txtGenre.Text.Trim(),
                    Year = year,
                    Available = chkAvailable.Checked
                };

                Album savedAlbum = await client.AddAlbumAsync(
                    album,
                    deadline: DateTime.UtcNow.AddSeconds(10));

                txtId.Text = savedAlbum.Id.ToString();

                MessageBox.Show("Album added.");
                RefreshAlbums(sender, e);
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Could not add album: " + ex.Status.Detail);
            }
            finally
            {
                btnAdd.Enabled = true;
            }
        }

        // Find an album and display its details.
        private async void FindAlbum(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show("Enter a valid album ID.");
                return;
            }

            btnFind.Enabled = false;

            try
            {
                Album album = await client.GetAlbumAsync(
                    new AlbumIdRequest { Id = id },
                    deadline: DateTime.UtcNow.AddSeconds(10));

                txtTitle.Text = album.Title;
                txtArtist.Text = album.Artist;
                txtGenre.Text = album.Genre;
                txtYear.Text = album.Year.ToString();
                chkAvailable.Checked = album.Available;
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Could not find album: " + ex.Status.Detail);
            }
            finally
            {
                btnFind.Enabled = true;
            }
        }

        // Save changes to an existing album.
        private async void UpdateAlbum(object sender, EventArgs e)
        {
            int id;
            int year;

            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show("Enter a valid album ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtArtist.Text) ||
                string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Enter a title, artist, and genre.");
                return;
            }

            if (!int.TryParse(txtYear.Text, out year) || year <= 0)
            {
                MessageBox.Show("Enter a valid year.");
                return;
            }

            btnUpdate.Enabled = false;

            try
            {
                Album album = new Album
                {
                    Id = id,
                    Title = txtTitle.Text.Trim(),
                    Artist = txtArtist.Text.Trim(),
                    Genre = txtGenre.Text.Trim(),
                    Year = year,
                    Available = chkAvailable.Checked
                };

                await client.UpdateAlbumAsync(
                    album,
                    deadline: DateTime.UtcNow.AddSeconds(10));

                MessageBox.Show("Album updated.");
                RefreshAlbums(sender, e);
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Could not update album: " + ex.Status.Detail);
            }
            finally
            {
                btnUpdate.Enabled = true;
            }
        }

        // Delete an album after confirmation.
        private async void DeleteAlbum(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show("Enter a valid album ID.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Delete album " + id + "?",
                "Confirm deletion",
                MessageBoxButtons.YesNo);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            btnDelete.Enabled = false;

            try
            {
                await client.DeleteAlbumAsync(
                    new AlbumIdRequest { Id = id },
                    deadline: DateTime.UtcNow.AddSeconds(10));

                txtId.Clear();
                txtTitle.Clear();
                txtArtist.Clear();
                txtGenre.Clear();
                txtYear.Clear();
                chkAvailable.Checked = true;

                MessageBox.Show("Album deleted.");
                RefreshAlbums(sender, e);
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Could not delete album: " + ex.Status.Detail);
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        // Refresh through one ongoing bidirectional stream.
        private async void WatchAlbums(object sender, EventArgs e)
        {
            CancellationToken token = watchCancellation.Token;

            try
            {
                using (var call = client.WatchAlbums(
                    cancellationToken: token))
                {
                    while (!token.IsCancellationRequested)
                    {
                        await call.RequestStream.WriteAsync(
                            new ListAlbumsRequest());

                        if (!await call.ResponseStream.MoveNext(token))
                        {
                            break;
                        }

                        if (token.IsCancellationRequested)
                        {
                            break;
                        }

                        dgvAlbums.DataSource = new List<Album>(
                            call.ResponseStream.Current.Albums);

                        await Task.Delay(3000, token);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal when closing the window.
            }
            catch (RpcException ex)
            {
                if (!token.IsCancellationRequested)
                {
                    MessageBox.Show(
                        "Automatic updates stopped: " + ex.Status.Detail);
                }
            }
        }

        private void BuildScreen()
        {
            Text = "Album Inventory";
            ClientSize = new Size(900, 580);
            StartPosition = FormStartPosition.CenterScreen;

            // Album details.
            txtId = AddInput("Album ID:", 20);
            txtTitle = AddInput("Title:", 60);
            txtArtist = AddInput("Artist:", 100);
            txtGenre = AddInput("Genre:", 140);
            txtYear = AddInput("Year:", 180);

            chkAvailable = new CheckBox
            {
                Text = "Available",
                Location = new Point(130, 220),
                AutoSize = true,
                Checked = true
            };
            Controls.Add(chkAvailable);

            // Buttons will call the server later.
            btnAdd = AddButton("Add", 20);
            btnFind = AddButton("Find by ID", 150);
            btnUpdate = AddButton("Update", 280);
            btnDelete = AddButton("Delete", 410);
            btnRefresh = AddButton("Refresh", 540);

            // Display albums returned by the server.
            dgvAlbums = new DataGridView
            {
                Location = new Point(20, 310),
                Size = new Size(860, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            Controls.Add(dgvAlbums);
        }

        // Create a label and textbox.
        private TextBox AddInput(string labelText, int top)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(20, top + 4),
                AutoSize = true
            };

            TextBox input = new TextBox
            {
                Location = new Point(130, top),
                Width = 300
            };

            Controls.Add(label);
            Controls.Add(input);

            return input;
        }

        // Create a button in the action row.
        private Button AddButton(string text, int left)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(left, 265),
                Size = new Size(120, 32)
            };

            Controls.Add(button);
            return button;
        }
    }
}
