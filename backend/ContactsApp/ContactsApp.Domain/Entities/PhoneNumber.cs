using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public class PhoneNumber
    {
        public string Number { get; private set; } = string.Empty;

        public string Type { get; private set; } = string.Empty;

        public bool IsPrimary { get; private set; }

        private PhoneNumber()
        {
        }

        public static PhoneNumber Create(string number, string type, bool isPrimary)
        {
            var phoneNumber = new PhoneNumber();
            phoneNumber.SetPhoneNumber(number, type, isPrimary);
            return phoneNumber;
        }

        public void SetPhoneNumber(string number, string type, bool isPrimary)
        {
            Number = number;
            Type = type;
            IsPrimary = isPrimary;
        }
    }
}
