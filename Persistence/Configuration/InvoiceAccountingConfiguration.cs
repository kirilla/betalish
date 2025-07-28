namespace Betalish.Persistence.Configuration;

class InvoiceAccountingConfiguration : IEntityTypeConfiguration<InvoiceAccounting>
{
    public void Configure(EntityTypeBuilder<InvoiceAccounting> builder)
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
