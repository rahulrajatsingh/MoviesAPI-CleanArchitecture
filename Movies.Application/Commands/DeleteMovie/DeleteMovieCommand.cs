using MediatR;
using Movies.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public DeleteMovieCommand(int id)
        {
            Id = id;
        }

    }
}
