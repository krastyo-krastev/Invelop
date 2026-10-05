using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public class Contact
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; } = string.Empty;

        public string Surname { get; private set; } = string.Empty;

        public DateOnly DateOfBirth { get; private set; }

        public string IBAN { get; set; } = string.Empty;

        public Address Address { get; set; }

        private readonly List<PhoneNumber> _phoneNumbers = new ();
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get => _phoneNumbers; }

        private Contact()
        {
            Address = Address.Create(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static Contact Create(string firstName, string surname, DateOnly dateOfBirth, string iban)
        {
            var contact = new Contact();
            contact.SetContact(firstName, surname, dateOfBirth, iban);
            return contact;
        }

        public void SetContact(string firstName, string surname, DateOnly dateOfBirth, string iban)
        {
            FirstName = firstName;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            IBAN = iban;
        }
    }
}
