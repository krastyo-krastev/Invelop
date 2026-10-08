using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateContactCommand> _validator;
        private readonly ILogger<CreateContactCommandHandler> _logger;

        public CreateContactCommandHandler(
            IContactRepository contactRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateContactCommand> validator,
            ILogger<CreateContactCommandHandler> logger
        )
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task HandleAsync(CreateContactCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling create contact command for {command.FirstName} {command.Surname}");

            // Validate the command
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
