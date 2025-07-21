namespace Betalish.Persistence.Configuration;

class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.Article.Name);

        builder.Property(p => p.UnitPrice)
            .HasPrecision(18, 2);

        builder.Property(p => p.VatValue)
            .HasPrecision(18, 2);

        builder.Property(p => p.UnitName)
            .HasMaxLength(MaxLengths.Domain.Article.UnitName);

        builder.Property(p => p.RevenueAccount)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);

        builder.Property(p => p.VatAccount)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);

        builder.HasMany(x => x.InvoiceTemplateRows)
            .WithOne(x => x.Article)
            .HasForeignKey(x => x.ArticleId);
    }
}
