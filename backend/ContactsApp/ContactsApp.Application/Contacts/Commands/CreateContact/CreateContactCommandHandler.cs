using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateContactCommandHandler> _logger;

        public CreateContactCommandHandler(
            IContactRepository contactRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateContactCommandHandler> logger
        )
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task HandleAsync(CreateContactCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling create contact command for {command.FirstName} {command.Surname}");

            // Map value objects 
            var address = Address.Create(command.Country, command.City, command.PostalCode, command.Street);
            var phoneNumbers = command.PhoneNumbers
                .Select(p => PhoneNumber.Create(p.Number, p.Type, p.IsPrimary))
                .ToList();

            // Create the contact entity
            var contact = Contact.Create(
                command.FirstName,
                command.Surname,
                command.DateOfBirth,
                address,
                command.IBAN,
                phoneNumbers
            );

            // Save the contact to the database 
            await _contactRepository.AddContact(contact, cancellationToken);
            var res = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
