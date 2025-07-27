namespace Betalish.Persistence.Configuration;

class InvoiceFeeConfiguration : IEntityTypeConfiguration<InvoiceFee>
{
    public void Configure(EntityTypeBuilder<InvoiceFee> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2);
    }
}
