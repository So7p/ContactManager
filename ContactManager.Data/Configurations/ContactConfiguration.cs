using ContactManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder) 
        { 
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.MobilePhone).IsRequired();
            builder.HasIndex(c => c.MobilePhone).IsUnique();

            builder.Property(c => c.JobTitle).IsRequired();

            builder.Property(c => c.BirthDate).IsRequired();
        }
    }
}