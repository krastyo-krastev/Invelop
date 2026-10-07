using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public sealed class PhoneNumber
    {
        public int Id { get; private set; }

        public string Number { get; private set; } = string.Empty;

        public string Type { get; private set; } = string.Empty;

        public bool IsPrimary { get; private set; }

        private PhoneNumber()
        {
        }

        public static PhoneNumber Create(string number, string type, bool isPrimary)
        {
            var phoneNumber = new PhoneNumber();
            phoneNumber.UpdatePhoneNumber(number, type, isPrimary);
            return phoneNumber;
        }

        public void UpdatePhoneNumber(string number, string type, bool isPrimary)
        {
            Number = number;
            Type = type;
            IsPrimary = isPrimary;
        }
    }
}
