using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Repositories.Base;
using Movies.Core.Repositories;
using Movies.Infrastructure.Repositories.Base;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Movies.Infrastructure.Extensions;
public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<MovieContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MoviesDb")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IMovieRepository, MovieRepository>();
        services.AddTransient<ITenantRepository, TenantRepository>();

        return services;
    }
}
