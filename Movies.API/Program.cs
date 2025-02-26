using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using Movies.Infrastructure.Extensions;
using Movies.Application.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureConfiguration(builder);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

        ConfigureServices(builder.Services, builder.Configuration);


        

        // Use Serilog as the logging provider
        builder.Host.UseSerilog();

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        ConfigureMiddleware(app);

        app.Run();
    }

    /// <summary>
    /// Configures application settings from appsettings.json, environment variables, and command-line arguments.
    /// </summary>
    private static void ConfigureConfiguration(WebApplicationBuilder builder)
    {
        builder.Configuration
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables()
               .AddCommandLine(Environment.GetCommandLineArgs());
    }

    /// <summary>
    /// Configures all services including authentication, infrastructure, and application services.
    /// </summary>
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(ConfigureSwagger);

        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices();

        ConfigureAuthentication(services, configuration);

        services.AddAuthorization();
    }

    /// <summary>
    /// Configures middleware components like Swagger, HTTPS redirection, authentication, and authorization.
    /// </summary>
    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }

    /// <summary>
    /// Configures Swagger for API documentation.
    /// </summary>
    private static void ConfigureSwagger(SwaggerGenOptions c)
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies API", Version = "v1" });

        // Add support for JWT Bearer Authentication
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your valid token in the text input below. Example: 'Bearer eyJhbGciOiJIUzI1...'"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    }

    /// <summary>
    /// Configures JWT authentication.
    /// </summary>
    private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]); // Ensure this is at least 32 characters long

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Prevents token expiration delay issues
                };
            });
    }
}
