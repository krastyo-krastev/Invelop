
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

#if DEBUG
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AngularDev", policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
#endif

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

#if DEBUG
        app.UseCors("AngularDev");
#endif
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
