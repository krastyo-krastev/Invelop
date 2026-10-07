using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContactsApp.Domain.Entities;

namespace ContactsApp.Infrastructure.Data.Configuration
{
    internal class PhoneNumberConfiguration
    {
        public PhoneNumberConfiguration(EntityTypeBuilder<PhoneNumber> modelBuilder)
        {
            modelBuilder.HasKey(p => p.Id);

            modelBuilder.Property(p => p.Number)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Property(p => p.Type)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Property(p => p.IsPrimary)
                .IsRequired();
        }
    }
}
