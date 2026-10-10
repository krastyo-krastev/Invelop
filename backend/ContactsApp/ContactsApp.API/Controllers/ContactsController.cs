using Microsoft.AspNetCore.Mvc;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;

namespace ContactsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ICommandHandler<CreateContactCommand> _createContactCommandHandler;
        private readonly IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>> _getContactsPaginatedHandler;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ICommandHandler<CreateContactCommand> createContactCommandHandler,
            IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>> getContactsPaginatedHandler,
            ILogger<ContactsController> logger   
        )
        {
            _createContactCommandHandler = createContactCommandHandler;
            _getContactsPaginatedHandler = getContactsPaginatedHandler;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateContacts([FromBody] ContactDTO contact, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received request to create contact: {@contact.FirstName} {@contact.Surname}");

            var command = new CreateContactCommand(contact.FirstName,
                contact.Surname,
                DateOnly.Parse(contact.DateOfBirth),
                contact.Country,
                contact.City,
                contact.PostalCode,
                contact.Street,
                contact.IBAN,
                contact.PhoneNumbers
            );

            await _createContactCommandHandler.HandleAsync(command, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ContactSummaryDTO>>> GetContacts([FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10, 
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogInformation($"Received request to get contacts with page: {page} and pageSize: {pageSize}", page, pageSize);

            Thread.Sleep(5000);

            var query = new GetContactsPaginatedQuery(page, pageSize);
            var result = await _getContactsPaginatedHandler.HandleAsync(query, cancellationToken);
            return Ok(result);
        }
    }
}
