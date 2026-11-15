namespace MusicRentalManagementAPI.Models
{
    // Stores information about an album rental
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }

        // Records when the album was rented
        public DateTime RentalDate { get; set; } = DateTime.Now;

        // Stays empty until the album is returned
        public DateTime? ReturnDate { get; set; }
    }
}
