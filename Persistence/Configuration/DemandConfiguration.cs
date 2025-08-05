namespace Betalish.Persistence.Configuration;

class DemandConfiguration : IEntityTypeConfiguration<Demand>
{
    public void Configure(EntityTypeBuilder<Demand> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
