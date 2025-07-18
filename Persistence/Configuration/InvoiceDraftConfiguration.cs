namespace Betalish.Persistence.Configuration;

class InvoiceDraftConfiguration : IEntityTypeConfiguration<InvoiceDraft>
{
    public void Configure(EntityTypeBuilder<InvoiceDraft> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        builder.HasMany(x => x.InvoiceDraftRows)
            .WithOne(x => x.InvoiceDraft)
            .HasForeignKey(x => x.InvoiceDraftId);
    }
}
