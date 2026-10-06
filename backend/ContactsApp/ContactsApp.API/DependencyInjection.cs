using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;

namespace ContactsApp.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateContactCommand>, CreateContactCommandHandler>();
            return services;
        }
    }
}
