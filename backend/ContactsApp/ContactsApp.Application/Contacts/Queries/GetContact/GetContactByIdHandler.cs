using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contacts.Queries.GetContact
{
    public class GetContactByIdHandler : IQueryHandler<GetContactByIdQuery, ContactDTO?>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<GetContactByIdQuery> _validator;
        private readonly ILogger<GetContactByIdHandler> _logger;

        public GetContactByIdHandler(IContactRepository contactRepository,
            IValidator<GetContactByIdQuery> validator,
            ILogger<GetContactByIdHandler> logger)
        {
            _contactRepository = contactRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ContactDTO?> HandleAsync(GetContactByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling GetContactByIdQuery for Id: {query.Id}");

            var validationResult = await _validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Invalid GetContactsPaginatedQuery parameters.");
                throw new ValidationException(validationResult.Errors);
            }

            return await _contactRepository.GetContactByIdAsync(query.Id, cancellationToken);
        }
    }
}
