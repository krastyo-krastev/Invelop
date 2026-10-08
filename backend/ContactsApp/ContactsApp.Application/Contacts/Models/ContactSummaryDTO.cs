using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public sealed record ContactSummaryDTO(int Id, 
        string FirstName,
        string Surname,
        string Country,
        string City,
        string PrimaryPhoneNumber
    );
}
