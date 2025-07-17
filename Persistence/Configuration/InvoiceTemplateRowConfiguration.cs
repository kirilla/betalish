namespace Betalish.Persistence.Configuration;

class InvoiceTemplateRowConfiguration : IEntityTypeConfiguration<InvoiceTemplateRow>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplateRow> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
