namespace Betalish.Persistence.Configuration;

class InvoiceDraftRowConfiguration : IEntityTypeConfiguration<InvoiceDraftRow>
{
    public void Configure(EntityTypeBuilder<InvoiceDraftRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ArticleName)
            .HasMaxLength(MaxLengths.Domain.Article.Name);

        builder.Property(p => p.Quantity)
            .HasPrecision(18, 2);

        builder.Property(p => p.UnitPrice)
            .HasPrecision(18, 2);

        builder.Property(p => p.Unit)
            .HasMaxLength(MaxLengths.Domain.Article.UnitName);

        builder.Property(p => p.VatPercentage)
            .HasPrecision(18, 2);

        builder.Property(p => p.NetAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.VatAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.RevenueAccount)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);

        builder.Property(p => p.VatAccount)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);
    }
}
