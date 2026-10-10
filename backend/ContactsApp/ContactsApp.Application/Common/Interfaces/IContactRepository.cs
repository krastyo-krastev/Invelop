using ContactsApp.Application.Contacts.Models;
using ContactsApp.Domain.Entities;

namespace ContactsApp.Application.Common.Interfaces
{
    public interface IContactRepository
    {
        Task AddContact(Contact contact, CancellationToken cancellationToken);

        Task<PaginatedResult<ContactSummaryDTO>> GetContactsPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken);

        Task<ContactDTO?> GetContactByIdAsync(int id, CancellationToken cancellationToken);
    }
}
