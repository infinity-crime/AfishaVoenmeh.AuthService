using Microsoft.OpenApi.Models;

namespace AfishaVoenmeh.AuthService.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddConfiguredSwagger();

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
}
