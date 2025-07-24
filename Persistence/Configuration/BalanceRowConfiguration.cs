namespace Betalish.Persistence.Configuration;

class BalanceRowConfiguration : IEntityTypeConfiguration<BalanceRow>
{
    public void Configure(EntityTypeBuilder<BalanceRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Amount)
            .HasPrecision(18, 2);
    }
}
