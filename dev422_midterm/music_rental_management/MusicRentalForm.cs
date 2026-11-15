using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace dev422_midterm.music_rental_management
{
    public partial class MusicRentalForm : Form
    {
        // Connects to the Music Rental API
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7024/")
        };

        public MusicRentalForm()
        {
            InitializeComponent();

            // Connect the rental buttons
            btnRentAlbum.Click += btnRentAlbum_Click;
            btnRefreshRentals.Click += btnRefreshRentals_Click;
            btnReturnAlbum.Click += btnReturnAlbum_Click;
        }

        // Adds a new music album
        private async void btnAddAlbum_Click(object sender, EventArgs e)
        {
            // Check that all album fields are valid
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtArtist.Text) ||
                string.IsNullOrWhiteSpace(txtGenre.Text) ||
                !int.TryParse(txtYear.Text, out int year) ||
                year < 1 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Please enter valid album information.");
                return;
            }

            // Create an album using the form inputs
            var album = new
            {
                Title = txtTitle.Text,
                Artist = txtArtist.Text,
                Genre = txtGenre.Text,
                Year = year,
                Available = true
            };

            try
            {
                // Convert the album to JSON
                string json = JsonConvert.SerializeObject(album);
                var content = new StringContent(
                    json, Encoding.UTF8, "application/json");

                // Send the album to the API
                var response = await client.PostAsync("api/music", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Album added successfully!");

                    // Clear the fields
                    txtTitle.Clear();
                    txtArtist.Clear();
                    txtGenre.Clear();
                    txtYear.Clear();

                    // Update the album table
                    await LoadAlbums();
                }
                else
                {
                    MessageBox.Show("Failed to add album.");
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }

        // Refreshes the available albums table
        private async void btnRefreshAlbums_Click(object sender, EventArgs e)
        {
            await LoadAlbums();
        }

        // Gets available albums from the API
        private async Task LoadAlbums()
        {
            try
            {
                var response = await client.GetAsync("api/music");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var albums =
                        JsonConvert.DeserializeObject<List<MusicAlbum>>(json);

                    // Display albums in the table
                    dgvAlbums.DataSource = null;
                    dgvAlbums.DataSource = albums;
                }
                else
                {
                    MessageBox.Show("Failed to load albums.");
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }

        // Deletes the selected album
        private async void btnDeleteAlbum_Click(object sender, EventArgs e)
        {
            // Make sure an album is selected
            if (dgvAlbums.SelectedRows.Count == 0 ||
                dgvAlbums.SelectedRows[0].DataBoundItem == null)
            {
                MessageBox.Show("Please select an album to delete.");
                return;
            }

            var album =
                (MusicAlbum)dgvAlbums.SelectedRows[0].DataBoundItem;

            // Ask before deleting the album
            var confirm = MessageBox.Show(
                "Are you sure you want to delete " + album.Title + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Delete the album through the API
                var response =
                    await client.DeleteAsync("api/music/" + album.Id);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Album deleted successfully!");

                    // Refresh the table after deleting
                    await LoadAlbums();
                }
                else
                {
                    MessageBox.Show("Failed to delete album.");
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }

        // Rents an available album to a user
        private async void btnRentAlbum_Click(object sender, EventArgs e)
        {
            // Check that both IDs are valid
            if (!int.TryParse(txtUserId.Text, out int userId) ||
                !int.TryParse(txtAlbumId.Text, out int albumId) ||
                userId <= 0 || albumId <= 0)
            {
                MessageBox.Show(
                    "Please enter a valid User ID and Album ID.");
                return;
            }

            // Create the rental information
            var rental = new
            {
                UserId = userId,
                AlbumId = albumId
            };

            try
            {
                // Convert the rental to JSON
                string json = JsonConvert.SerializeObject(rental);
                var content = new StringContent(
                    json, Encoding.UTF8, "application/json");

                // Send the rental to the API
                var response =
                    await client.PostAsync("api/rentals", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Album rented successfully!");

                    // Clear the input fields
                    txtUserId.Clear();
                    txtAlbumId.Clear();

                    // Update both tables
                    await LoadAlbums();
                    await LoadRentals();
                }
                else
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show("Could not rent album: " + error);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }

        // Refreshes the active rentals table
        private async void btnRefreshRentals_Click(object sender, EventArgs e)
        {
            await LoadRentals();
        }

        // Gets active rentals from the API
        private async Task LoadRentals()
        {
            try
            {
                var response = await client.GetAsync("api/rentals");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var rentals =
                        JsonConvert.DeserializeObject<List<RentalRecord>>(json);

                    // Display rentals in the table
                    dgvRentals.DataSource = null;
                    dgvRentals.DataSource = rentals;
                }
                else
                {
                    MessageBox.Show("Failed to load rentals.");
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }

        // Returns the selected album
        private async void btnReturnAlbum_Click(object sender, EventArgs e)
        {
            // Check that a rental is selected
            if (dgvRentals.SelectedRows.Count == 0 ||
                dgvRentals.SelectedRows[0].DataBoundItem == null)
            {
                MessageBox.Show("Please select a rental to return.");
                return;
            }

            var rental =
                (RentalRecord)dgvRentals.SelectedRows[0].DataBoundItem;

            // Confirm the return
            var confirm = MessageBox.Show(
                "Are you sure you want to return this album?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Send the return request to the API
                var response = await client.PostAsync(
                    "api/rentals/" + rental.Id + "/return", null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Album returned successfully!");

                    // Update both tables
                    await LoadRentals();
                    await LoadAlbums();
                }
                else
                {
                    MessageBox.Show("Failed to return album.");
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Could not connect to the Music Rental API.");
            }
        }
    }

    // Represents an album returned by the API
    public class MusicAlbum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }
    }

    // Represents a rental returned by the API
    public class RentalRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
