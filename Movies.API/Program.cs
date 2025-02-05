using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MediatR;
using AutoMapper;
using System.Reflection;
using Movies.Infrastructure.Data;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Repositories.Base;
using Movies.Infrastructure.Repositories;
using Movies.Core.Repositories;
using Movies.Application.Handlers.CommandHandlers;

var builder = WebApplication.CreateBuilder(args);


// Load configuration from appsettings.json and environment variables
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
       .AddEnvironmentVariables()
       .AddCommandLine(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesDb")));

// Explicitly specify AutoMapper's method to resolve ambiguity
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

// Register MediatR
// Register MediatR and scan the Movies.Application assembly for handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateMovieCommandHandler>());


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IMovieRepository), typeof(MovieRepository));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
