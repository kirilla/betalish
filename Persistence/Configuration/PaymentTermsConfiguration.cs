namespace Betalish.Persistence.Configuration;

class PaymentTermsConfiguration : IEntityTypeConfiguration<PaymentTerms>
{
    public void Configure(EntityTypeBuilder<PaymentTerms> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.BillingStrategy.Name);

        builder.Property(p => p.MinToConsiderPaid)
            .HasPrecision(18, 2);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.BillingStrategy)
            .HasForeignKey(x => x.BillingStrategyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
