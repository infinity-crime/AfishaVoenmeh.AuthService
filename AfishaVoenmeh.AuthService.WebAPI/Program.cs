using AfishaVoenmeh.AuthService.Application;
using AfishaVoenmeh.AuthService.Infrastructure;
using AfishaVoenmeh.AuthService.WebAPI;
using AfishaVoenmeh.AuthService.WebAPI.Middeware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if(app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    await app.InitAndRunAsync();
}
