using ContactsApp.Application.Abstractions;
using ContactsApp.Application.Contacts.Models;

namespace ContactsApp.Application.Contacts.Commands.CreateContact
{
    public sealed record CreateContactCommand(string FirstName, 
        string Surname,
        DateOnly DateOfBirth,
        string IBAN,
        IReadOnlyCollection<PhoneNumberDTO> PhoneNumbers,
        string Country,
        string City,
        string PostalCode,
        string Street
    ) : ICommand;
}
