using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public sealed class Contact
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; } = string.Empty;

        public string Surname { get; private set; } = string.Empty;

        public DateOnly DateOfBirth { get; private set; }

        public Address Address { get; set; }
        
        public string IBAN { get; set; } = string.Empty;

        private readonly List<PhoneNumber> _phoneNumbers = new ();
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get => _phoneNumbers; }

        private Contact()
        {
            Address = Address.Create(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static Contact Create(string firstName, 
            string surname, 
            DateOnly dateOfBirth,
            string country,
            string city,
            string postalCode,
            string street,
            string iban,
            IReadOnlyCollection<PhoneNumber> phoneNumbers
        )
        {
            var contact = new Contact();
            contact.UpdatePersonalDetails(firstName, surname, dateOfBirth);
            contact.UpdateAddress(country, city, postalCode, street);
            contact.UpdateBankDetails(iban);
            contact.UpdatePhoneNumbers(phoneNumbers);
            return contact;
        }

        public void UpdatePersonalDetails(string firstName, string surname, DateOnly dateOfBirth)
        {
            FirstName = firstName;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public void UpdateAddress(string country, string city, string postalCode, string street)
        {
            Address.SetAddress(country, city, postalCode, street);
        }

        public void UpdateBankDetails(string iban)
        {
            IBAN = iban;
        }

        public void UpdatePhoneNumbers(IReadOnlyCollection<PhoneNumber> phoneNumbers)
        {
            _phoneNumbers.Clear();
            _phoneNumbers.AddRange(phoneNumbers);
        }
    }
}
