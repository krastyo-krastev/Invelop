using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Models;

namespace ContactsApp.Application.Contacts.Queries.GetContact
{
    public sealed record GetContactByIdQuery(int Id) : IQuery<ContactDTO>;
}
