using ContactsApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Domain.Entities
{
    public sealed class Address
    {
        public string Country { get; private set; } = string.Empty;

        public string City { get; private set; } = string.Empty;

        public string PostalCode { get; private set; } = string.Empty;

        public string Street { get; private set; } = string.Empty;

        private Address()
        {
        }

        /// <summary>
        /// Address factory method to create a new Address instance.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="postalCode"></param>
        /// <param name="street"></param>
        /// <returns></returns>
        public static Address Create(string country, string city, string postalCode, string street)
        {
            var address = new Address();
            address.SetAddress(country, city, postalCode, street);
            return address;
        }

        /// <summary>
        /// Set address details and apply business rules if needed. 
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="postalCode"></param>
        /// <param name="street"></param>
        public void SetAddress(string country, string city, string postalCode, string street)
        {
            // TODO: Apply business rules here if needed, e.g., validation of postal code format, etc.

            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
        }

        public void SetAddress(Address address)
        {
            if (address == null)
            {
                throw new DomainException("The address cannot be empty.");
            }
            SetAddress(address.Country, address.City, address.PostalCode, address.Street);
        }
    }
}
