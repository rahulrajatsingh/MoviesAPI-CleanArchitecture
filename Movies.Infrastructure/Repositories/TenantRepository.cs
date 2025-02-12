using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Authorization;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories.Base;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    private readonly MovieContext _context;

    public TenantRepository(MovieContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ValidateClientAsync(string clientId, string clientSecret)
    {
        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
        {
            return false;
        }

        return await _context.Tenants
            .AnyAsync(t => t.ClientId == clientId && t.ClientSecret == clientSecret);
    }
}
