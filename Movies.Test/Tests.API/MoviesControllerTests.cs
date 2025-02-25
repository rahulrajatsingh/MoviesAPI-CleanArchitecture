using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.API.Controllers;
using Movies.Application.Commands;
using Movies.Application.Commands.CreateMovie;
using Movies.Application.Commands.DeleteMovie;
using Movies.Application.Commands.UpdateMovie;
using Movies.Application.Queries.GetAllMovies;
using Movies.Application.Queries.GetMovieById;
using Movies.Application.Responses;

namespace Movies.Test.Tests.API
{
    public class MoviesControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private MoviesController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new MoviesController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetAllMovies_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<MovieResponse>
            {
                new MovieResponse { Id = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" },
                new MovieResponse { Id = 2, MovieName = "Interstellar", DirectorName = "Christopher Nolan" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllMoviesQuery>(), default))
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
            var movie = new MovieResponse { Id = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" };

            _mediatorMock.Setup(m => m.Send(It.Is<GetMovieByIdQuery>(q => q.Id == 1), default))
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMovieByIdQuery>(), default))
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
            var command = new CreateMovieCommand { MovieName = "Inception", DirectorName = "Christopher Nolan" };
            var movieResponse = new MovieResponse { Id = 1, MovieName = "Inception", DirectorName = "Christopher Nolan" };

            _mediatorMock.Setup(m => m.Send(command, default))
                         .ReturnsAsync(movieResponse);

            // Act
            var result = await _controller.CreateMovie(command);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Value.Should().BeEquivalentTo(movieResponse);
        }

        [Test]
        public async Task UpdateMovie_ShouldReturnUpdatedMovie_WhenMovieExists()
        {
            // Arrange
            var command = new UpdateMovieCommand { Id = 1, MovieName = "Updated Title", DirectorName = "Updated Director" };
            var updatedMovie = new MovieResponse { Id = 1, MovieName = "Updated Title", DirectorName = "Updated DirectorName" };

            _mediatorMock.Setup(m => m.Send(command, default))
                         .ReturnsAsync(updatedMovie);

            // Act
            var result = await _controller.UpdateMovie(1, command);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedMovie);
        }

        [Test]
        public async Task DeleteMovie_ShouldReturnNoContent_WhenMovieIsDeleted()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMovieCommand>(), default))
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMovieCommand>(), default))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteMovie(99);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}