using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //services.AddScoped<>();
            return services;
        }
    }
}
