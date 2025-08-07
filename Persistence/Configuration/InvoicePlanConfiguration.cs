namespace Betalish.Persistence.Configuration;

class InvoicePlanConfiguration : IEntityTypeConfiguration<InvoicePlan>
{
    public void Configure(EntityTypeBuilder<InvoicePlan> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.MinToConsiderPaid)
            .HasPrecision(18, 2);

        builder.Property(p => p.ReminderFee)
            .HasPrecision(18, 2);

        builder.Property(p => p.DemandFee)
            .HasPrecision(18, 2);
    }
}
