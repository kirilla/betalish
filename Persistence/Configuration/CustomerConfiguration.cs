namespace Betalish.Persistence.Configuration;

class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Customer.Name);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Customer.Address);
    }
}
