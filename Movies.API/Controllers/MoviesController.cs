using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands.CreateMovie;
using Movies.Application.Commands.DeleteMovie;
using Movies.Application.Commands.UpdateMovie;
using Movies.Application.Queries;
using Movies.Application.Queries.GetAllMovies;
using Movies.Application.Queries.GetMovieByDirectorName;
using Movies.Application.Queries.GetMovieById;
using Movies.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [JwtAuthFilter] only needed of the scheme is not added in the middleware
    [Authorize] // this will work if the scheme is added in middleware
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor to inject Mediator (which is provided by the dependency injection container)
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/movies/{directorName}
        [HttpGet("director/{directorName}")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMoviesByDirectorName(string directorName)
        {
            var query = new GetMoviesByDirectorNameQuery(directorName);  // Passing director name to the query
            var result = await _mediator.Send(query);
            return Ok(result);  // Return the list of movies for the director
        }

        // GET api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies()
        {
            var query = new GetAllMoviesQuery(); // Query to get all movies
            var result = await _mediator.Send(query);
            return Ok(result);  // Return all movies in response
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponse>> GetMovieById(int id)
        {
            var query = new GetMovieByIdQuery(id);  // Query to fetch a movie by its ID
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();  // Return 404 if movie not found
            }

            return Ok(result);  // Return the movie response
        }

        // POST api/movies
        [HttpPost]
        public async Task<ActionResult<MovieResponse>> CreateMovie([FromBody] CreateMovieCommand command)
        {
            var movie = await _mediator.Send(command);  // Handle the create command via Mediator
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);  // Return 201 with location header
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieResponse>> UpdateMovie(int id, [FromBody] UpdateMovieCommand command)
        {
            command.Id = id;  // Set the ID of the movie in the command
            var movie = await _mediator.Send(command);  // Send the update command

            if (movie == null)
            {
                return NotFound();  // Return 404 if movie not found
            }

            return Ok(movie);  // Return updated movie
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var command = new DeleteMovieCommand(id);  // Create the delete command with movie ID
            var result = await _mediator.Send(command);  // Send the delete command

            if (!result)
            {
                return NotFound();  // Return 404 if movie not found
            }

            return NoContent();  // Return 204 if successfully deleted
        }
    }

}
