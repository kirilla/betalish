namespace Betalish.Persistence.Configuration;

class BillingStrategyConfiguration : IEntityTypeConfiguration<BillingStrategy>
{
    public void Configure(EntityTypeBuilder<BillingStrategy> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.BillingStrategy.Name);
    }
}
