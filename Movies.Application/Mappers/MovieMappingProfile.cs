using AutoMapper;
using Movies.Application.Models.Request;
using Movies.Application.Models.Response;
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
            // Entity <-> Response
            CreateMap<Movie, MovieResponse>().ReverseMap();

            // Request <-> Entity
            CreateMap<MovieRequest, Movie>().ReverseMap();

            // **Explicitly map collections**
            CreateMap<IEnumerable<Movie>, List<MovieResponse>>()
                .ConvertUsing((src, dest, context) => context.Mapper.Map<List<MovieResponse>>(src));
        }
    }

}
