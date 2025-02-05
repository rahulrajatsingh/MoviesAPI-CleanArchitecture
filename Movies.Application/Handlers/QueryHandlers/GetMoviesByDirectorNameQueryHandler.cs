using MediatR;
using Movies.Application.Mappers;
using Movies.Application.Queries;
using Movies.Application.Responses;
using Movies.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Handlers.QueryHandlers
{
    public class GetMoviesByDirectorNameQueryHandler : IRequestHandler<GetMoviesByDirectorNameQuery, IEnumerable<MovieResponse>>
    {
        public readonly IMovieRepository _movieRepository;

        public GetMoviesByDirectorNameQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieResponse>> Handle(GetMoviesByDirectorNameQuery request, CancellationToken cancellationToken)
        {
            var movieList = await _movieRepository.GetMoviesByDirectorName(request.DirectorName);
            var movieResponseList = MovieMapper.Mapper.Map<IEnumerable<MovieResponse>>(movieList);
            return movieResponseList;

        }
    }
}
