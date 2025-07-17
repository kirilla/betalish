namespace Betalish.Persistence.Configuration;

class InvoiceDraftConfiguration : IEntityTypeConfiguration<InvoiceDraft>
{
    public void Configure(EntityTypeBuilder<InvoiceDraft> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
