using AutoMapper;
using Movies.Application.Models.Request;
using Movies.Application.Models.Response;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieResponse>>(movies);
        }

        public async Task<MovieResponse> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie == null ? null : _mapper.Map<MovieResponse>(movie);
        }

        public async Task<IEnumerable<MovieResponse>> GetMoviesByDirectorAsync(string directorName)
        {
            var movies = await _movieRepository.GetMoviesByDirectorName(directorName);
            return _mapper.Map<IEnumerable<MovieResponse>>(movies);
        }

        public async Task<MovieResponse> AddMovieAsync(MovieRequest movieRequest)
        {
            var movieEntity = _mapper.Map<Movie>(movieRequest);
            var createdMovie = await _movieRepository.AddAsync(movieEntity);
            return _mapper.Map<MovieResponse>(createdMovie);
        }

        public async Task<MovieResponse> UpdateMovieAsync(int id, MovieRequest movieRequest)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null) return null;

            _mapper.Map(movieRequest, existingMovie); // Updates existing entity
            var updatedMovie = await _movieRepository.UpdateAsync(existingMovie);
            return _mapper.Map<MovieResponse>(updatedMovie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return false;

            await _movieRepository.DeleteAsync(movie);
            return true;
        }
    }
}
