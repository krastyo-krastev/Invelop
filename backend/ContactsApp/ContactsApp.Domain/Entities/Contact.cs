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

        public Address? Address { get; set; }

        private readonly List<PhoneNumber> _phoneNumbers = new ();
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers { get => _phoneNumbers; }

        private Contact()
        {
        }

        public static Contact Create(string firstName, string surname, DateOnly dateOfBirth)
        {
            var contact = new Contact();
            contact.SetContact(firstName, surname, dateOfBirth);
            return contact;
        }

        public void SetContact(string firstName, string surname, DateOnly dateOfBirth)
        {
            FirstName = firstName;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }
    }
}
