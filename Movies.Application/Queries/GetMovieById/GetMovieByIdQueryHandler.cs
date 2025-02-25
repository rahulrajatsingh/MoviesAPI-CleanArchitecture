using MediatR;
using Movies.Application.Mappers;
using Movies.Application.Responses;
using Movies.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries.GetMovieById
{
    internal class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieResponse>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movieEntity = await _movieRepository.GetByIdAsync(request.Id);

            if (movieEntity == null)
            {
                throw new ArgumentException($"Movie with ID {request.Id} not found");
            }

            var movieResponse = MovieMapper.Mapper.Map<MovieResponse>(movieEntity);
            return movieResponse;
        }
    }
}
