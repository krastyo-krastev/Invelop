using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public class Address
    {
        public string Country { get; private set; } = string.Empty;

        public string City { get; private set; } = string.Empty;

        public string PostalCode { get; private set; } = string.Empty;

        public string Street { get; private set; } = string.Empty;

        private Address()
        {
        }

        public static Address Create(string country, string city, string postalCode, string street)
        {
            var address = new Address();
            address.SetAddress(country, city, postalCode, street);
            return address;
        }

        public void SetAddress(string country, string city, string postalCode, string street)
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
        }
    }
}
