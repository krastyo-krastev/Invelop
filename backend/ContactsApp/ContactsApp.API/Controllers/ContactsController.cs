using Azure.Core;
using ContactsApp.API.Models;
using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Commands.CreateContact;
using ContactsApp.Application.Contacts.Commands.UpdateContact;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Application.Contacts.Queries.GetContact;
using ContactsApp.Application.Contacts.Queries.GetContactsPaginated;
using Microsoft.AspNetCore.Mvc;

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
            _logger.LogInformation($"Received request to get contact with ID: {id}");

            var query = new GetContactByIdQuery(id);
            var result = await _getContactByIdHandler.HandleAsync(query, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateContacts([FromBody] ContactDTO contact, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received request to create contact: {@contact.FirstName} {@contact.Surname}");

            if (contact == null)
            {
                var requestId = HttpContext.TraceIdentifier;
                var errorResponse = ErrorResponse.ArgumentError("Wrong or missing contact data", requestId);
                return BadRequest(errorResponse);
            }

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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContact([FromBody] ContactDTO contact, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received request to update contact with ID: {contact?.Id}");

            if (contact == null)
            {
                var requestId = HttpContext.TraceIdentifier;
                var errorResponse = ErrorResponse.ArgumentError("Wrong or missing contact data", requestId);
                return BadRequest(errorResponse);
            }

            var command = new UpdateContactCommand(contact.Id,
                contact.FirstName,
                contact.Surname,
                DateOnly.Parse(contact.DateOfBirth),
                contact.Country,
                contact.City,
                contact.PostalCode,
                contact.Street,
                contact.IBAN,
                contact.PhoneNumbers);

            return Ok();
        }
    }
}
