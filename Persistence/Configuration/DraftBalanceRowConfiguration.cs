namespace Betalish.Persistence.Configuration;

class DraftBalanceRowConfiguration : IEntityTypeConfiguration<DraftBalanceRow>
{
    public void Configure(EntityTypeBuilder<DraftBalanceRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Amount)
            .HasPrecision(18, 2);
    }
}
