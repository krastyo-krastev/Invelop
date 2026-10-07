using ContactsApp.Domain.Entities;

namespace ContactsApp.Application.Common.Interfaces
{
    public interface IContactRepository
    {
        Task AddContact(Contact contact, CancellationToken cancellationToken);
    }
}
