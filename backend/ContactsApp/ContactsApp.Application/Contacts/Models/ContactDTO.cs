using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public record ContactDTO(string FirstName,
        string Surname,
        DateOnly DateOfBirth,
        string IBAN,
        IReadOnlyCollection<PhoneNumberDTO> PhoneNumbers,
        string Country,
        string City,
        string PostalCode,
        string Street
    );
}
