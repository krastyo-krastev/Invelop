using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public sealed record PhoneNumberDTO(int Id, string Number, string Type, bool IsPrimary);
}
