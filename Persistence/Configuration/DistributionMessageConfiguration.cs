namespace Betalish.Persistence.Configuration;

class DistributionMessageConfiguration : IEntityTypeConfiguration<DistributionMessage>
{
    public void Configure(EntityTypeBuilder<DistributionMessage> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
