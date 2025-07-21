namespace Betalish.Persistence.Configuration;

class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasIndex(p => p.InvoiceNumber)
           .IsUnique()
           .HasFilter("[InvoiceNumber] IS NOT NULL");

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        builder.HasMany(x => x.InvoiceRows)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);
    }
}
