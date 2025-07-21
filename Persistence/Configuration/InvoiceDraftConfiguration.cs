namespace Betalish.Persistence.Configuration;

class InvoiceDraftConfiguration : IEntityTypeConfiguration<InvoiceDraft>
{
    public void Configure(EntityTypeBuilder<InvoiceDraft> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        // Summary
        builder.Property(p => p.NetAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.VatAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Total)
            .HasPrecision(18, 2);

        builder.Property(p => p.TotalRounding)
            .HasPrecision(18, 2);

        // Relations
        builder.HasMany(x => x.InvoiceDraftRows)
            .WithOne(x => x.InvoiceDraft)
            .HasForeignKey(x => x.InvoiceDraftId);
    }
}
