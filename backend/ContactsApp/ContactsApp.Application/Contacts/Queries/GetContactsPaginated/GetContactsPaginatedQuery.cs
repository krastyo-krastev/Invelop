using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Models;

namespace ContactsApp.Application.Contacts.Queries.GetContactsPaginated
{
    public sealed record GetContactsPaginatedQuery(int Page, int PageSize) : IQuery<PaginatedResult<ContactSummaryDTO>>;
}
