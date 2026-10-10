using Microsoft.Extensions.DependencyInjection;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using FluentValidation;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContact;

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

            services.AddScoped<IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>>, GetContactsPaginatedHandler>();
            services.AddScoped<IValidator<GetContactsPaginatedQuery>, GetContactsPaginatedValidator>();

            services.AddScoped<IQueryHandler<GetContactByIdQuery, ContactDTO>, GetContactByIdHandler>();
            services.AddScoped<IValidator<GetContactByIdQuery>, GetContactByIdValidator>();

            return services;
        }
    }
}
