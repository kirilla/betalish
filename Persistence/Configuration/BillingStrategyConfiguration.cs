namespace Betalish.Persistence.Configuration;

class BillingStrategyConfiguration : IEntityTypeConfiguration<BillingStrategy>
{
    public void Configure(EntityTypeBuilder<BillingStrategy> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.BillingStrategy.Name);

        builder.Property(p => p.MinToConsiderPaid)
            .HasPrecision(18, 2);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.BillingStrategy)
            .HasForeignKey(x => x.BillingStrategyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
