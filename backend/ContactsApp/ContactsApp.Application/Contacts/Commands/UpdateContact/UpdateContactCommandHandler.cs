using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Exceptions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Models;
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

            var contact = await _contactRepository.GetContactByIdForUpdateAsync(command.Id, cancellationToken);
            if (contact == null)
            {
                _logger.LogWarning($"Contact not found for update, id {command.Id}");
                throw new NotFoundException("Contact not found for update");
            }

            // Update contact details
            contact.SetPersonalDetails(command.FirstName, command.Surname, command.DateOfBirth);
            contact.SetAddress(command.Country, command.City, command.PostalCode, command.Street);
            contact.SetBankDetails(command.IBAN);

            // Create new list from DTOs to update
            var updatedPhonesList = command.PhoneNumbers
                .Select(p => new PhoneNumberData(p.Id, p.Number, p.Type, p.IsPrimary))
                .ToList();
            contact.UpdatePhoneNumbers(updatedPhonesList);

            // Save the updated contact to the database 
            var res = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
