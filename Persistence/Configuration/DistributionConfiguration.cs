namespace Betalish.Persistence.Configuration;

class DistributionConfiguration : IEntityTypeConfiguration<Distribution>
{
    public void Configure(EntityTypeBuilder<Distribution> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
