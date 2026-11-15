namespace MusicRentalManagementAPI.Models
{
    // Stores information about a music album
    public class Music
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Genre { get; set; } = "";
        public int Year { get; set; }

        // Tracks whether the album is available to rent
        public bool Available { get; set; } = true;
    }
}
