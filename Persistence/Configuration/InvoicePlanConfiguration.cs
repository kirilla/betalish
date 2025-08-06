namespace Betalish.Persistence.Configuration;

class InvoicePlanConfiguration : IEntityTypeConfiguration<InvoicePlan>
{
    public void Configure(EntityTypeBuilder<InvoicePlan> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
