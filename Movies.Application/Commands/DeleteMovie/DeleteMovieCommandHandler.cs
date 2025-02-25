using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Movies.Application.Mappers;
using Movies.Application.Responses;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Result<bool>>
    {
        public readonly IMovieRepository _movieRepository;
        private readonly IValidator<DeleteMovieCommand> _validator;
        public DeleteMovieCommandHandler(IMovieRepository movieRepository, IValidator<DeleteMovieCommand> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            // Run FluentValidation before executing logic
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                // Return structured validation errors instead of throwing an exception
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<bool>.Failure(errors);
            }

            // Fetch entity
            var movieEntity = await _movieRepository.GetByIdAsync(request.Id);
            if (movieEntity == null)
            {
                // Instead of throwing an exception, return false or handle gracefully
                return Result<bool>.Failure(new List<string> { $"Movie with ID {request.Id} not found." });
            }

            bool deleted = await _movieRepository.DeleteAsync(movieEntity);
            return Result<bool>.Success(deleted);
        }
    }
}
