using Movies.Application.Models.Request;
using Movies.Application.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponse>> GetAllMoviesAsync();
        Task<MovieResponse> GetMovieByIdAsync(int id);
        Task<IEnumerable<MovieResponse>> GetMoviesByDirectorAsync(string directorName);
        Task<MovieResponse> AddMovieAsync(MovieRequest movieRequest);
        Task<MovieResponse> UpdateMovieAsync(int id, MovieRequest movieRequest);
        Task<bool> DeleteMovieAsync(int id);
    }
}
