using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContactsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure.Data.Configuration
{
    internal class ContactConfiguration
    {
        public ContactConfiguration(EntityTypeBuilder<Contact> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);

            modelBuilder.Property(c => c.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Property(c => c.Surname)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Property(c => c.DateOfBirth)
                .IsRequired();

            modelBuilder.ComplexProperty(p => p.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasMaxLength(200)
                    .IsRequired();

                address.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();

                address.Property(a => a.PostalCode)
                    .HasMaxLength(20);

                address.Property(a => a.Country)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Property(c => c.IBAN)
                .HasMaxLength(34)
                .IsRequired();

            modelBuilder.HasMany(c => c.PhoneNumbers)
                .WithOne()
                .HasForeignKey("ContactId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
