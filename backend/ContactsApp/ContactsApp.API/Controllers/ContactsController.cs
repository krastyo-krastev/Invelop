using Microsoft.AspNetCore.Mvc;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;
using ContactsApp.Application.Contacts.Queries.GetContact;

namespace ContactsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ICommandHandler<CreateContactCommand> _createContactCommandHandler;
        private readonly IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>> _getContactsPaginatedHandler;
        private readonly IQueryHandler<GetContactByIdQuery, ContactDTO> _getContactByIdHandler;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ICommandHandler<CreateContactCommand> createContactCommandHandler,
            IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>> getContactsPaginatedHandler,
            IQueryHandler<GetContactByIdQuery, ContactDTO> getContactByIdHandler,
            ILogger<ContactsController> logger   
        )
        {
            _createContactCommandHandler = createContactCommandHandler;
            _getContactsPaginatedHandler = getContactsPaginatedHandler;
            _getContactByIdHandler = getContactByIdHandler;
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

            var query = new GetContactsPaginatedQuery(page, pageSize);
            var result = await _getContactsPaginatedHandler.HandleAsync(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContactById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received request to get contact with ID: {id}", id);

            var query = new GetContactByIdQuery(id);
            var result = await _getContactByIdHandler.HandleAsync(query, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
