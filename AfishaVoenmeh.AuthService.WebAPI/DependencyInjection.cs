using AfishaVoenmeh.AuthService.WebAPI.Common;
using Mapster;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AfishaVoenmeh.AuthService.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddConfiguredSwagger();

        services.AddAsyncInitializer<DbInitializer>();

        services.AddMapings();

        return services;
    }

    private static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AuthService (AfishaVoenmeh)",
                Version = "v1",
                Description = "A microservice for user authentication in the AfishaVoenmekh system."
            });
        });

        return services;
    }

    private static IServiceCollection AddMapings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddMapster();

        return services;
    }
}
