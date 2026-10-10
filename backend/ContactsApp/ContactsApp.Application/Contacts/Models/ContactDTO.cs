using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public sealed record ContactDTO(
        int Id,
        string FirstName,
        string Surname,
        string DateOfBirth,
        string Country,
        string City,
        string PostalCode,
        string Street,
        string IBAN,
        IReadOnlyCollection<PhoneNumberDTO> PhoneNumbers
    );
}
