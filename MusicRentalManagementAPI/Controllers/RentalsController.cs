using Microsoft.AspNetCore.Mvc;
using MusicRentalManagementAPI.Models;

namespace MusicRentalManagementAPI.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        // Stores rental records while the API is running
        private static List<Rental> rentals = new List<Rental>();

        private static int nextId = 1;

        // GET: api/rentals
        // Lists all active rentals
        [HttpGet]
        public IActionResult GetRentals()
        {
            var activeRentals = rentals
                .Where(r => r.ReturnDate == null)
                .ToList();

            return Ok(activeRentals);
        }

        // POST: api/rentals
        // Rents an available album to a user
        [HttpPost]
        public IActionResult RentAlbum(Rental rental)
        {
            var album = MusicController.Albums
                .FirstOrDefault(a => a.Id == rental.AlbumId);

            if (album == null)
            {
                return NotFound("Album not found.");
            }

            if (!album.Available)
            {
                return BadRequest("This album is already rented.");
            }

            if (rental.UserId <= 0)
            {
                return BadRequest("Please enter a valid user ID.");
            }

            rental.Id = nextId++;
            rental.RentalDate = DateTime.Now;
            rental.ReturnDate = null;

            // Mark the album as rented
            album.Available = false;

            rentals.Add(rental);

            return Ok(rental);
        }

        // POST: api/rentals/{id}/return
        // Returns an album and makes it available again
        [HttpPost("{id}/return")]
        public IActionResult ReturnAlbum(int id)
        {
            var rental = rentals.FirstOrDefault(r => r.Id == id);

            if (rental == null)
            {
                return NotFound("Rental not found.");
            }

            if (rental.ReturnDate != null)
            {
                return BadRequest("This album has already been returned.");
            }

            rental.ReturnDate = DateTime.Now;

            // Make the album available again
            var album = MusicController.Albums
                .FirstOrDefault(a => a.Id == rental.AlbumId);

            if (album != null)
            {
                album.Available = true;
            }

            return Ok("Album returned successfully.");
        }
    }
}
