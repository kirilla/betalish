namespace Betalish.Persistence.Configuration;

class DistributionTriggerConfiguration : IEntityTypeConfiguration<DistributionTrigger>
{
    public void Configure(EntityTypeBuilder<DistributionTrigger> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
