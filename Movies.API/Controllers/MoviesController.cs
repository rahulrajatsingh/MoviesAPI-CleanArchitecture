using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models.Request;
using Movies.Application.Models.Response;
using Movies.Application.Services;
using Movies.Core.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly Core.Logging.ILogger _logger;

        public MoviesController(IMovieService movieService, Core.Logging.ILogger logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet("director/{directorName}")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMoviesByDirectorName(string directorName)
        {
            var movies = await _movieService.GetMoviesByDirectorAsync(directorName);
            return Ok(movies);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies()
        {
            _logger.LogInfo("Fetching all movies...");
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponse>> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> CreateMovie([FromBody] MovieRequest movieRequest)
        {
            var movieResponse = await _movieService.AddMovieAsync(movieRequest);
            return CreatedAtAction(nameof(GetMovieById), new { id = movieResponse.ID }, movieResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieResponse>> UpdateMovie(int id, [FromBody] MovieRequest movieRequest)
        {
            var updatedMovie = await _movieService.UpdateMovieAsync(id, movieRequest);
            if (updatedMovie == null) return NotFound();
            return Ok(updatedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
