using Microsoft.AspNetCore.Mvc;
using MusicRentalManagementAPI.Models;

namespace MusicRentalManagementAPI.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        // Stores the music albums while the API is running
        public static List<Music> Albums = new List<Music>();

        private static int nextId = 1;

        // GET: api/music
        // Lists all available albums
        [HttpGet]
        public IActionResult GetMusic()
        {
            return Ok(Albums.Where(a => a.Available).ToList());
        }

        // POST: api/music
        // Adds a new album to the library
        [HttpPost]
        public IActionResult AddMusic(Music album)
        {
            if (string.IsNullOrWhiteSpace(album.Title) ||
                string.IsNullOrWhiteSpace(album.Artist) ||
                string.IsNullOrWhiteSpace(album.Genre) ||
                album.Year <= 0)
            {
                return BadRequest("Please enter valid album information.");
            }

            album.Id = nextId++;
            album.Available = true;

            Albums.Add(album);

            return Ok(album);
        }

        // DELETE: api/music/{id}
        // Deletes an album if it is available
        [HttpDelete("{id}")]
        public IActionResult DeleteMusic(int id)
        {
            var album = Albums.FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                return NotFound("Album not found.");
            }

            if (!album.Available)
            {
                return BadRequest("Cannot delete an album that is rented.");
            }

            Albums.Remove(album);

            return Ok("Album deleted successfully.");
        }
    }
}
