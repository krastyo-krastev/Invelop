using Microsoft.EntityFrameworkCore;
using ContactsApp.Application.Common.Interfaces;
using ContactsApp.Domain.Entities;
using ContactsApp.Infrastructure.Data.Configuration;

namespace ContactsApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ContactConfiguration(modelBuilder.Entity<Contact>());
            new PhoneNumberConfiguration(modelBuilder.Entity<PhoneNumber>());
        }
    }
}
