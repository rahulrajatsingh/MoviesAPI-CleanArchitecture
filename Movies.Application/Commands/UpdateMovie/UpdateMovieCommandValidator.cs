using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Movies.Application.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("fluent - Movie ID must be greater than zero.");

        RuleFor(x => x.MovieName)
            .NotEmpty().WithMessage("Movie name is required.")
            .MaximumLength(200).WithMessage("Movie name must not exceed 200 characters.");

        RuleFor(x => x.DirectorName)
            .NotEmpty().WithMessage("Director name is required.")
            .MaximumLength(100).WithMessage("Director name must not exceed 100 characters.");

        RuleFor(x => x.ReleaseYear)
            .NotEmpty().WithMessage("Release year is required.")
            .Matches(@"^\d{4}$").WithMessage("Release year must be a valid 4-digit year.");
    }
}

