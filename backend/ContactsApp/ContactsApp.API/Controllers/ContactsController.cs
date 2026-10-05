using Microsoft.AspNetCore.Mvc;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using ContactsApp.Application.Contacts.Models;

namespace ContactsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ICommandHandler<CreateContactCommand> _createContactCommandHandler;

        public ContactsController(ICommandHandler<CreateContactCommand> createContactCommandHandler)
        {
            _createContactCommandHandler = createContactCommandHandler;
        }

        [HttpPost]
        public async Task<ActionResult> CreateContacts([FromBody] ContactDTO contact, CancellationToken cancellationToken)
        {
            var command = new CreateContactCommand(contact.FirstName,
                contact.Surname,
                contact.DateOfBirth,
                contact.IBAN,
                contact.PhoneNumbers,
                contact.Country,
                contact.City,
                contact.PostalCode,
                contact.Street
            );

            await _createContactCommandHandler.HandleAsync(command, cancellationToken);

            return Ok();
        }
    }
}
