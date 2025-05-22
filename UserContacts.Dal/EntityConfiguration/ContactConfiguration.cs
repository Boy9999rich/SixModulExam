using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserContacts.Dal.Entities;

namespace UserContacts.Dal.EntityConfiguration;

public class ContactConfiguration : IEntityTypeConfiguration<Contacts>
{
    public void Configure(EntityTypeBuilder<Contacts> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(b => b.Id);

        builder.Property(c => c.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.LastName)
               .HasMaxLength(100);

        builder.Property(c => c.PhoneNumber)
               .HasMaxLength(50);

        builder.Property(c => c.Email)
               .HasMaxLength(100);

        builder.Property(c => c.Address)
               .HasMaxLength(200);

        builder.Property(c => c.CreatedAt)
               .HasDefaultValueSql("GETDATE()");
    }
}
