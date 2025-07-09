namespace Betalish.Persistence.Configuration;

class NetworkRequestConfiguration : IEntityTypeConfiguration<NetworkRequest>
{
    public void Configure(EntityTypeBuilder<NetworkRequest> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Url)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Http.Url);

        builder.Property(p => p.Method)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Http.Method);

        builder.Property(p => p.IpAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);

        builder.Property(p => p.UserAgent)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Http.UserAgent);
    }
}
