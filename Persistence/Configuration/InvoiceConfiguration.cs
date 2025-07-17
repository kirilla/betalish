namespace Betalish.Persistence.Configuration;

class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
