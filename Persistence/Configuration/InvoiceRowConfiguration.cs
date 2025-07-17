namespace Betalish.Persistence.Configuration;

class InvoiceRowConfiguration : IEntityTypeConfiguration<InvoiceRow>
{
    public void Configure(EntityTypeBuilder<InvoiceRow> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
