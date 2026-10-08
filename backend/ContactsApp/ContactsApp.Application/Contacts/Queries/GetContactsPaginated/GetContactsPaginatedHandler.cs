using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Application.Contacts.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contacts.Queries.GetContactsPaginated
{
    public class GetContactsPaginatedHandler : IQueryHandler<GetContactsPaginatedQuery, PaginatedResult<ContactSummaryDTO>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<GetContactsPaginatedQuery> _validator;
        private readonly ILogger<GetContactsPaginatedHandler> _logger;

        public GetContactsPaginatedHandler(IContactRepository contactRepository,
            IValidator<GetContactsPaginatedQuery> validator,
            ILogger<GetContactsPaginatedHandler> logger
        )
        {
            _contactRepository = contactRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<PaginatedResult<ContactSummaryDTO>> HandleAsync(GetContactsPaginatedQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handle contacts paginated query, page {query.Page}, pageSize {query.PageSize}");

            var validationResult = await _validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Invalid GetContactsPaginatedQuery parameters.");
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _contactRepository.GetContactsPaginatedAsync(query.Page, query.PageSize, cancellationToken);

            return result;
        }
    }
}
