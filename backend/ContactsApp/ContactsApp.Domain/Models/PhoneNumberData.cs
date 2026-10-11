using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Models
{
    public sealed record PhoneNumberData(int Id, string Number, string Type, bool IsPrimary);
}
