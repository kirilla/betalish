namespace Betalish.Persistence.Configuration;

class ClientAuthConfiguration : IEntityTypeConfiguration<ClientAuth>
{
    public void Configure(EntityTypeBuilder<ClientAuth> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
