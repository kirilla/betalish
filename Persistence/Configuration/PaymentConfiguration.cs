namespace Betalish.Persistence.Configuration;

class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Reference)
            .HasMaxLength(MaxLengths.Domain.Payment.Reference);

        builder.HasMany(x => x.PaymentAccountingRows)
            .WithOne(x => x.Payment)
            .HasForeignKey(x => x.PaymentId);
    }
}
