
using ContactsApp.API.Middleware;
using ContactsApp.Application;
using ContactsApp.Infrastructure;

namespace ContactsApp.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AngularRequest", policy =>
            {
                var origins = builder.Environment.IsDevelopment()
                            ? new[] { "http://localhost:4200" }
                            // TODO: Update the origin URL to match your Angular app's URL in production
                            : new[] { "https://contacts.example.com" };

                policy
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Add error handling middleware early in pipeline
        app.UseMiddleware<ErrorHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseCors("AngularRequest");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
