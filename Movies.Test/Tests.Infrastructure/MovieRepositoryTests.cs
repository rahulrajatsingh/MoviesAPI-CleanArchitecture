using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Movies.Test.Tests.Infrastructure;

[TestFixture]
public class MovieRepositoryTests
{
    private MovieContext _movieContext;
    private MovieRepository _repository;
    private List<Movie> _movies;

    [SetUp]
    public void Setup()
    {
        // Create in-memory database options
        var options = new DbContextOptionsBuilder<MovieContext>()
            .UseInMemoryDatabase(databaseName: "TestMovieDb") // 👈 Use In-Memory Database
            .Options;

        // Create actual DbContext using in-memory DB
        _movieContext = new MovieContext(options);

        // Ensure database is empty before each test
        _movieContext.Database.EnsureDeleted();
        _movieContext.Database.EnsureCreated();

        // Seed test data
        _movies = new List<Movie>
        {
            new Movie { ID = 1, MovieName = "Inception", DirectorName = "Christopher Nolan", ReleaseYear = "2010" },
            new Movie { ID = 2, MovieName = "Dunkirk", DirectorName = "Christopher Nolan", ReleaseYear = "2017" },
            new Movie { ID = 3, MovieName = "Avatar", DirectorName = "James Cameron", ReleaseYear = "2009" }
        };

        _movieContext.Movies.AddRange(_movies);
        _movieContext.SaveChanges(); // Save seeded data

        // Create repository with real context
        _repository = new MovieRepository(_movieContext);
    }

    [TearDown]
    public void TearDown()
    {
        _movieContext.Dispose(); // ✅ Dispose the DbContext to avoid NUnit1032
    }

    [Test]
    public async Task AddAsync_Should_Add_Movie()
    {
        // Arrange
        var newMovie = new Movie { ID = 4, MovieName = "Interstellar", DirectorName = "Christopher Nolan", ReleaseYear = "2014" };

        // Act
        await _repository.AddAsync(newMovie);
        var moviesInDb = await _repository.GetAllAsync();

        // Assert
        Assert.That(moviesInDb.Count(), Is.EqualTo(4));
        Assert.That(moviesInDb.Any(m => m.MovieName == "Interstellar"), Is.True);
    }

    [Test]
    public async Task DeleteAsync_Should_Remove_Movie()
    {
        // Arrange
        var movieToRemove = _movies[0];

        // Act
        var result = await _repository.DeleteAsync(movieToRemove);
        var moviesInDb = await _repository.GetAllAsync();

        // Assert
        Assert.That(result, Is.True);
        Assert.That(moviesInDb.Contains(movieToRemove), Is.False);
    }

    [Test]
    public async Task GetAllAsync_Should_Return_All_Movies()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        CollectionAssert.AreEqual(_movies.OrderBy(m => m.ID).ToList(),
                          result.OrderBy(m => m.ID).ToList());
    }

    [Test]
    public async Task GetByIdAsync_Should_Return_Correct_Movie()
    {
        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.MovieName, Is.EqualTo("Inception"));
    }

    [Test]
    public async Task UpdateAsync_Should_Modify_Existing_Movie()
    {
        // Arrange
        var movie = await _repository.GetByIdAsync(1);
        var updatedMovie = movie;

        // Act
        updatedMovie.MovieName = "Inception 2";
        updatedMovie.ReleaseYear = "2025";

        var result = await _repository.UpdateAsync(updatedMovie);

        var retrievedMovie = await _repository.GetByIdAsync(1);

        // Assert
        Assert.That(retrievedMovie, Is.Not.Null);
        Assert.That(retrievedMovie.MovieName, Is.EqualTo("Inception 2"));
        Assert.That(retrievedMovie.ReleaseYear, Is.EqualTo("2025"));
    }

    [Test]
    public async Task GetMoviesByDirectorName_Should_Return_Correct_Movies()
    {
        // Act
        var result = await _repository.GetMoviesByDirectorName("Christopher Nolan");

        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.Any(m => m.MovieName == "Inception"), Is.True);
        Assert.That(result.Any(m => m.MovieName == "Dunkirk"), Is.True);
    }
}
