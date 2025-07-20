namespace Betalish.Persistence.Configuration;

class InvoiceRangeConfiguration : IEntityTypeConfiguration<InvoiceRange>
{
    public void Configure(EntityTypeBuilder<InvoiceRange> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Comment)
            .HasMaxLength(MaxLengths.Domain.InvoiceRange.Comment);
    }
}
