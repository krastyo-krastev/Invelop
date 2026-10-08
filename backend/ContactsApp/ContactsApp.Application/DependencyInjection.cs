using Microsoft.Extensions.DependencyInjection;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using FluentValidation;

namespace ContactsApp.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Configure DI services for the application layer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateContactCommand>, CreateContactCommandHandler>();
            services.AddScoped<IValidator<CreateContactCommand>, CreateContactCommandValidator>();

            return services;
        }
    }
}
