using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public class PhoneNumber
    {
        public string Value { get; private set; } = string.Empty;

        private PhoneNumber()
        {
        }

        public static PhoneNumber Create(string value)
        {
            var phoneNumber = new PhoneNumber();
            phoneNumber.SetPhoneNumber(value);
            return phoneNumber;
        }

        public void SetPhoneNumber(string value)
        {
            Value = value;
        }
    }
}
