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

        builder.Property(p => p.EmailAddress)
            .HasMaxLength(MaxLengths.Domain.Customer.EmailAddress);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);
    }
}
