namespace Betalish.Persistence.Configuration;

class InvoiceDraftRowConfiguration : IEntityTypeConfiguration<InvoiceDraftRow>
{
    public void Configure(EntityTypeBuilder<InvoiceDraftRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity).HasPrecision(18, 2);
    }
}
