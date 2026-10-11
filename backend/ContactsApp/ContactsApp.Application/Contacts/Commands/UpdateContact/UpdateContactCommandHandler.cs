using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : ICommandHandler<UpdateContactCommand>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateContactCommand> _validator;
        private readonly ILogger<UpdateContactCommandHandler> _logger;

        public UpdateContactCommandHandler(
            IContactRepository contactRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateContactCommand> validator,
            ILogger<UpdateContactCommandHandler> logger
        )
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateContactCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling update contact command for {command.FirstName} {command.Surname}");

            // Validate the command
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Invalid UpdateContactCommand parameters.");
                throw new ValidationException(validationResult.Errors);
            }

/* KMK
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
*/
            // Save the contact to the database 
//            await _contactRepository.UpdateContact(contact, cancellationToken);
            var res = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
