using MediatR;
using Movies.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries.GetAllMovies
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<MovieResponse>>
    {
    }
}
