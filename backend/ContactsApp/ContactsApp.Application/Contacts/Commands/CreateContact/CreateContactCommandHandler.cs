using ContactsApp.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
    {
        public async Task HandleAsync(CreateContactCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
