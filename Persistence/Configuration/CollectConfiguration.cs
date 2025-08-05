namespace Betalish.Persistence.Configuration;

class CollectConfiguration : IEntityTypeConfiguration<Collect>
{
    public void Configure(EntityTypeBuilder<Collect> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
