using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Queries.GetContact
{
    public class GetContactByIdValidator : AbstractValidator<GetContactByIdQuery>
    {
        public GetContactByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Contact Id is required.");
        }
    }
}
