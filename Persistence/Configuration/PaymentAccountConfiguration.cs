namespace Betalish.Persistence.Configuration;

class PaymentAccountConfiguration : IEntityTypeConfiguration<PaymentAccount>
{
    public void Configure(EntityTypeBuilder<PaymentAccount> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.PaymentAccount.Name);

        builder.Property(p => p.Description)
            .HasMaxLength(MaxLengths.Domain.PaymentAccount.Description);
    }
}
