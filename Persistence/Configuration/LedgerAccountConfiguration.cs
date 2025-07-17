namespace Betalish.Persistence.Configuration;

class LedgerAccountConfiguration : IEntityTypeConfiguration<LedgerAccount>
{
    public void Configure(EntityTypeBuilder<LedgerAccount> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Account)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);

        builder.Property(p => p.Description)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Description);
    }
}
