using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContact;
using ContactsApp.Application.Contacts.Commands.UpdateContact;

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
            // Commands

            services.AddScoped<ICommandHandler<CreateContactCommand>, CreateContactCommandHandler>();
            services.AddScoped<IValidator<CreateContactCommand>, CreateContactCommandValidator>();

            services.AddScoped<ICommandHandler<UpdateContactCommand>, UpdateContactCommandHandler>();
            services.AddScoped<IValidator<UpdateContactCommand>, UpdateContactCommandValidator>();

            // Queries

            services.AddScoped<IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>>, GetContactsPaginatedHandler>();
            services.AddScoped<IValidator<GetContactsPaginatedQuery>, GetContactsPaginatedValidator>();

            services.AddScoped<IQueryHandler<GetContactByIdQuery, ContactDTO?>, GetContactByIdHandler>();
            services.AddScoped<IValidator<GetContactByIdQuery>, GetContactByIdValidator>();

            return services;
        }
    }
}
