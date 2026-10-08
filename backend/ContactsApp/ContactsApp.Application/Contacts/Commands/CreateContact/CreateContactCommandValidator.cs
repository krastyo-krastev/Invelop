using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(100).WithMessage("Surname cannot exceed 100 characters.");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .MaximumLength(20).WithMessage("Postal code cannot exceed 20 characters.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

            RuleFor(c => c.IBAN)
                .NotEmpty().WithMessage("IBAN is required.")
                .MaximumLength(34).WithMessage("IBAN must be 34 characters long.");

            RuleFor(c => c.PhoneNumbers)
                .NotEmpty().WithMessage("At least one phone number is required.")
                .Must(phoneNumbers => phoneNumbers.Count > 0).WithMessage("At least one phone number is required.");

            RuleForEach(x => x.PhoneNumbers)
                .ChildRules(phone =>
                {
                    phone.RuleFor(x => x.Number)
                        .NotEmpty().WithMessage("Phone number is required.")
                        .MaximumLength(30).WithMessage("Phone number cannot exceed 30 characters.");

                    phone.RuleFor(x => x.Type)
                        .NotEmpty().WithMessage("Phone number type is required.")
                        .MaximumLength(20).WithMessage("Phone number type cannot exceed 20 characters.");
                });
        }
    }
}
