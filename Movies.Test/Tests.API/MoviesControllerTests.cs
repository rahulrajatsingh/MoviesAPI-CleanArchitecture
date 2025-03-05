using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.API.Controllers;
using Movies.Application.Models.Request;
using Movies.Application.Models.Response;
using Movies.Application.Services;
using Movies.Core.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Test.Tests.API
{
    public class MoviesControllerTests
    {
        private Mock<IMovieService> _movieServiceMock;
        private Mock<ILogger> _logMock;
        private MoviesController _controller;

        [SetUp]
        public void Setup()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _logMock = new Mock<ILogger>();
            _controller = new MoviesController(_movieServiceMock.Object, _logMock.Object);
        }

        [Test]
        public async Task GetAllMovies_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<MovieResponse>
            {
                new MovieResponse { ID = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" },
                new MovieResponse { ID = 2, MovieName = "Interstellar", DirectorName = "Christopher Nolan" }
            };

            _movieServiceMock.Setup(service => service.GetAllMoviesAsync())
                             .ReturnsAsync(movies);

            // Act
            var result = await _controller.GetAllMovies();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(movies);
        }

        [Test]
        public async Task GetMovieById_ShouldReturnMovie_WhenFound()
        {
            // Arrange
            var movie = new MovieResponse { ID = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" };

            _movieServiceMock.Setup(service => service.GetMovieByIdAsync(1))
                             .ReturnsAsync(movie);

            // Act
            var result = await _controller.GetMovieById(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(movie);
        }

        [Test]
        public async Task GetMovieById_ShouldReturnNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            _movieServiceMock.Setup(service => service.GetMovieByIdAsync(99))
                             .ReturnsAsync((MovieResponse)null);

            // Act
            var result = await _controller.GetMovieById(99);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task CreateMovie_ShouldReturnCreatedResponse_WhenMovieIsCreated()
        {
            // Arrange
            var request = new MovieRequest { MovieName = "Inception", DirectorName = "Christopher Nolan" };
            var movieResponse = new MovieResponse { ID = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" };

            _movieServiceMock.Setup(service => service.AddMovieAsync(request))
                             .ReturnsAsync(movieResponse);

            // Act
            var result = await _controller.CreateMovie(request);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Value.Should().BeEquivalentTo(movieResponse);
        }

        [Test]
        public async Task UpdateMovie_ShouldReturnUpdatedMovie_WhenMovieExists()
        {
            // Arrange
            var request = new MovieRequest { MovieName = "Updated Title", DirectorName = "Updated Director" };
            var updatedMovie = new MovieResponse { ID = 1, MovieName = "Updated Title", DirectorName = "Updated Director" };

            _movieServiceMock.Setup(service => service.UpdateMovieAsync(1, request))
                             .ReturnsAsync(updatedMovie);

            // Act
            var result = await _controller.UpdateMovie(1, request);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedMovie);
        }

        [Test]
        public async Task UpdateMovie_ShouldReturnNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            var request = new MovieRequest { MovieName = "Updated Title", DirectorName = "Updated Director" };

            _movieServiceMock.Setup(service => service.UpdateMovieAsync(99, request))
                             .ReturnsAsync((MovieResponse)null);

            // Act
            var result = await _controller.UpdateMovie(99, request);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task DeleteMovie_ShouldReturnNoContent_WhenMovieIsDeleted()
        {
            // Arrange
            _movieServiceMock.Setup(service => service.DeleteMovieAsync(1))
                             .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteMovie(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task DeleteMovie_ShouldReturnNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            _movieServiceMock.Setup(service => service.DeleteMovieAsync(99))
                             .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteMovie(99);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
