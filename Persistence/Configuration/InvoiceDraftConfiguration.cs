namespace Betalish.Persistence.Configuration;

class InvoiceDraftConfiguration : IEntityTypeConfiguration<InvoiceDraft>
{
    public void Configure(EntityTypeBuilder<InvoiceDraft> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        builder.Property(p => p.PaymentTerms)
            .HasMaxLength(MaxLengths.Common.Invoice.PaymentTerms);

        // Summary
        builder.Property(p => p.NetAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.VatAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Total)
            .HasPrecision(18, 2);

        builder.Property(p => p.TotalRounding)
            .HasPrecision(18, 2);

        // Customer address
        builder.Property(p => p.Customer_Address1)
            .HasMaxLength(MaxLengths.Common.Address.Address1);

        builder.Property(p => p.Customer_Address2)
            .HasMaxLength(MaxLengths.Common.Address.Address2);

        builder.Property(p => p.Customer_City)
            .HasMaxLength(MaxLengths.Common.Address.City);

        builder.Property(p => p.Customer_Country)
            .HasMaxLength(MaxLengths.Common.Address.Country);

        builder.Property(p => p.Customer_ZipCode)
            .HasMaxLength(MaxLengths.Common.Address.ZipCode);

        // Customer email
        builder.Property(p => p.Customer_Email)
            .HasMaxLength(MaxLengths.Common.Email.Address);

        // Relations
        builder.HasMany(x => x.InvoiceDraftRows)
            .WithOne(x => x.InvoiceDraft)
            .HasForeignKey(x => x.InvoiceDraftId);
    }
}
