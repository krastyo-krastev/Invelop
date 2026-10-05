using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public record PhoneNumberDTO(string Number, string Type, bool IsPrimary);
}
