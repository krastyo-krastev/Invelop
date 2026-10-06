using ContactsApp.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand>
    {
        private readonly ILogger<CreateContactCommandHandler> _logger;

        public CreateContactCommandHandler(ILogger<CreateContactCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(CreateContactCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling create contact command for {command.FirstName} {command.Surname}");
            throw new NotImplementedException();
        }
    }
}
