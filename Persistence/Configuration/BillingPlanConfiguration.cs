namespace Betalish.Persistence.Configuration;

class BillingPlanConfiguration : IEntityTypeConfiguration<BillingPlan>
{
    public void Configure(EntityTypeBuilder<BillingPlan> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.BillingPlan.Name);

        builder.HasMany(x => x.BillingPlanItems)
            .WithOne(x => x.BillingPlan)
            .HasForeignKey(x => x.BillingPlanId);
    }
}
