namespace Betalish.Persistence.Configuration;

class ClientEventConfiguration : IEntityTypeConfiguration<ClientEvent>
{
    public void Configure(EntityTypeBuilder<ClientEvent> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.ClientEvent.Description);

        builder.Property(p => p.IpAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);
    }
}
