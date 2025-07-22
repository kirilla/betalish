namespace Betalish.Persistence.Configuration;

class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Ssn10)
            .HasMaxLength(MaxLengths.Common.Ssn.Ssn10);

        builder.Property(p => p.Orgnum)
            .HasMaxLength(MaxLengths.Common.Organization.Orgnum);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Customer.Name);

        // Customer address
        builder.Property(p => p.Address1)
            .HasMaxLength(MaxLengths.Common.Address.Address1);

        builder.Property(p => p.Address2)
            .HasMaxLength(MaxLengths.Common.Address.Address2);

        builder.Property(p => p.City)
            .HasMaxLength(MaxLengths.Common.Address.City);

        builder.Property(p => p.Country)
            .HasMaxLength(MaxLengths.Common.Address.Country);

        builder.Property(p => p.ZipCode)
            .HasMaxLength(MaxLengths.Common.Address.ZipCode);

        // Customer email
        builder.Property(p => p.EmailAddress)
            .HasMaxLength(MaxLengths.Domain.Customer.EmailAddress);

        builder.HasMany(x => x.CustomerTags)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
    }
}
