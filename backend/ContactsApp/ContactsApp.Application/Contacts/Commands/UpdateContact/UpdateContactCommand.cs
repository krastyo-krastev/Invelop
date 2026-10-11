using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Models;

namespace ContactsApp.Application.Contacts.Commands.UpdateContact
{
    public sealed record UpdateContactCommand(int Id,
        string FirstName,
        string Surname,
        DateOnly DateOfBirth,
        string Country,
        string City,
        string PostalCode,
        string Street,
        string IBAN,
        IReadOnlyCollection<PhoneNumberDTO> PhoneNumbers
    ) : ICommand;
}
