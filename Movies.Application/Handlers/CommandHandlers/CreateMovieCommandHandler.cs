using MediatR;
using Movies.Application.Commands;
using Movies.Application.Mappers;
using Movies.Application.Responses;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Handlers.CommandHandlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieResponse>
    {
        public readonly IMovieRepository _movieRepository;

        public CreateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieEntity = MovieMapper.Mapper.Map<Movie>(request);
            if (movieEntity == null)
            {
                throw new ArgumentException("Problem with mapping");
            }

            var createdMovie = await _movieRepository.AddAsync(movieEntity);
            MovieResponse response = MovieMapper.Mapper.Map<MovieResponse>(movieEntity);
            return response;
        }
    }


}
