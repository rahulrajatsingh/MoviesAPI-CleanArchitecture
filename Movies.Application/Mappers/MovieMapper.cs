using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Mappers
{
    public class MovieMapper
    {
        private static readonly Lazy<IMapper> _lazyMapper = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MovieMappingProfile>(); // Add your mapping profile(s) here
                                                       // Only map public properties (ignores private fields/properties)
                cfg.ShouldMapProperty = prop => prop.CanRead && prop.CanWrite && prop.GetMethod.IsPublic;
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazyMapper.Value;
    }
}
