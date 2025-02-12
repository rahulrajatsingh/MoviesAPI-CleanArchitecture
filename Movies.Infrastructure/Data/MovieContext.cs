using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Core.Entities;
using Movies.Authorization;

namespace Movies.Infrastructure.Data
{
    public class MovieContext :DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MoviesDb;User ID=sa;Password=p@ssw0rd000;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
    }
}
