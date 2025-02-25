using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(movie => movie.MovieName)
            .NotEmpty().WithMessage("fluent - Movie name is required.")
            .MaximumLength(100).WithMessage("Movie name cannot exceed 100 characters.");

        RuleFor(movie => movie.DirectorName)
            .NotEmpty().WithMessage("Director name is required.")
            .MaximumLength(100).WithMessage("Director name cannot exceed 100 characters.");

        RuleFor(movie => movie.ReleaseYear)
            .NotEmpty().WithMessage("Release year is required.")
            .Matches(@"^\d{4}$").WithMessage("Release year must be a valid 4-digit number.");
    }
}
