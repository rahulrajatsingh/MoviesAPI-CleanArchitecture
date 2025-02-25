using AutoMapper;
using Movies.Application.Commands.CreateMovie;
using Movies.Application.Commands.DeleteMovie;
using Movies.Application.Commands.UpdateMovie;
using Movies.Application.Responses;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Mappers
{
    public class MovieMappingProfile : Profile
    {

        public MovieMappingProfile() 
        {
            CreateMap<Movie, CreateMovieCommand>().ReverseMap();
            CreateMap<Movie, DeleteMovieCommand>().ReverseMap();
            CreateMap<Movie, UpdateMovieCommand>().ReverseMap();
            CreateMap<Movie, MovieResponse>().ReverseMap();
        }        
    }
}
