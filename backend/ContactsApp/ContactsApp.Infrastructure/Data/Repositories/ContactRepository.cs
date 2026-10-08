using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Application.Contacts.Models;
using ContactsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<PaginatedResult<ContactSummaryDTO>> GetContactsPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Contacts
                .AsNoTracking()
                .OrderBy(x => x.Surname);

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ContactSummaryDTO(
                    c.Id,
                    c.FirstName,
                    c.Surname,
                    c.Address.Country,
                    c.Address.City,
                    c.PhoneNumbers.Where(p => p.IsPrimary)
                        .Select(p => p.Number)
                        .SingleOrDefault() ?? string.Empty
                ))
                .ToListAsync(cancellationToken);

            return new PaginatedResult<ContactSummaryDTO>(
                data,
                page,
                pageSize,
                totalCount);
        }
    }
}
