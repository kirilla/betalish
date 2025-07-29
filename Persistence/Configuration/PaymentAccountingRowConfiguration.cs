namespace Betalish.Persistence.Configuration;

class PaymentAccountingRowConfiguration : IEntityTypeConfiguration<PaymentAccountingRow>
{
    public void Configure(EntityTypeBuilder<PaymentAccountingRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Account)
            .HasMaxLength(MaxLengths.Common.Bookkeeping.Account);

        builder.Property(p => p.Credit)
            .HasPrecision(18, 2);

        builder.Property(p => p.Debit)
            .HasPrecision(18, 2);
    }
}
