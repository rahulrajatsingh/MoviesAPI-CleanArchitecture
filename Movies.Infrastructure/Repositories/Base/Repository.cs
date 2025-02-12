using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected MovieContext MovieContext { get; private set; }

        public Repository(MovieContext movieContext)
        {
            MovieContext = movieContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await MovieContext.Set<T>().AddAsync(entity);
            await MovieContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            MovieContext.Set<T>().Remove(entity);
            await MovieContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await MovieContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await MovieContext.Set<T>().FirstOrDefaultAsync(e => EF.Property<int>(e, "ID") == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            MovieContext.Entry(entity).State = EntityState.Modified;
            await MovieContext.SaveChangesAsync();
            return entity;
        }
    }
}
