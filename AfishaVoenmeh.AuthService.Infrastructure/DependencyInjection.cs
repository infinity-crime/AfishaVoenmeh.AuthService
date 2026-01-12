using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Authentication;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using AfishaVoenmeh.AuthService.Infrastructure.Authentication;
using AfishaVoenmeh.AuthService.Infrastructure.Authentication.Common;
using AfishaVoenmeh.AuthService.Infrastructure.Data;
using AfishaVoenmeh.AuthService.Infrastructure.Data.Repositories;
using AfishaVoenmeh.AuthService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AfishaVoenmeh.AuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddApplicationDbContext(configuration);
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddJwtBearerAuth(configuration);

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }

    private static void AddJwtBearerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration[JwtOptions.IssuerSection],
                    ValidAudience = configuration[JwtOptions.AudienceSection],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                        .GetBytes(configuration[JwtOptions.IssuerSigningKeySection]!))
                };
            });
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultPostgresConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}