namespace Betalish.Persistence.Configuration;

class BillingPlanItemConfiguration : IEntityTypeConfiguration<BillingPlanItem>
{
    public void Configure(EntityTypeBuilder<BillingPlanItem> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
