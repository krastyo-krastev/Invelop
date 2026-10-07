using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Infrastructure.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContact(Contact contact, CancellationToken cancellationToken)
        {
            await _context.Contacts.AddAsync(contact, cancellationToken);
        }
    }
}
