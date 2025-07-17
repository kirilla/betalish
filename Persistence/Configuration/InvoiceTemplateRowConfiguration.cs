namespace Betalish.Persistence.Configuration;

class InvoiceTemplateRowConfiguration : IEntityTypeConfiguration<InvoiceTemplateRow>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplateRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity).HasPrecision(18, 2);
    }
}
