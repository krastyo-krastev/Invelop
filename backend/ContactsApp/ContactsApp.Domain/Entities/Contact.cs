using ContactsApp.Domain.Exceptions;
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

        private const int MinAge = 16;


        /// <summary>
        /// Private constructor
        /// </summary>
        private Contact()
        {
            Address = Address.Create(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Contact factory method to creates a new instance of Contact with the provided details.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="address"></param>
        /// <param name="iban"></param>
        /// <returns></returns>
        public static Contact Create(string firstName, 
            string surname, 
            DateOnly dateOfBirth,
            Address address,
            string iban,
            IReadOnlyCollection<PhoneNumber> phoneNumbers
        )
        {
            var contact = new Contact();
            contact.SetPersonalDetails(firstName, surname, dateOfBirth);
            contact.SetAddress(address);
            contact.SetBankDetails(iban);
            contact.AddPhoneNumbers(phoneNumbers);
            return contact;
        }

        public void SetPersonalDetails(string firstName, string surname, DateOnly dateOfBirth)
        {
            // The contact must be at least 16 years old
            var sixteen = dateOfBirth.AddYears(MinAge).ToDateTime(TimeOnly.MinValue);
            if (sixteen > DateTime.Now)
            {
                throw new DomainException("The contact must be at least 16 years old.");
            }

            FirstName = firstName;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public void SetAddress(Address address)
        {
            Address.SetAddress(address);
        }

        public void SetBankDetails(string iban)
        {
            IBAN = iban;
        }

        /// <summary>
        /// Reset the phone numbers list and add new ones.
        /// </summary>
        /// <param name="phoneNumbers"></param>
        public void AddPhoneNumbers(IReadOnlyCollection<PhoneNumber> phoneNumbers)
        {
            // The contact should have at least one phone number
            if (phoneNumbers == null || phoneNumbers.Count == 0)
            {
                throw new DomainException("The contact must have at least one phone number.");
            }

            // The contact should have exactly one primary phone number
            if (phoneNumbers.Where(p => p.IsPrimary).Count() != 1)
            {
                throw new DomainException("The contact must have exactly one primary phone number.");
            }

            _phoneNumbers.Clear();
            foreach(var phoneNumber in phoneNumbers)
            {
                _phoneNumbers.Add(phoneNumber);
            }
        }
    }
}
