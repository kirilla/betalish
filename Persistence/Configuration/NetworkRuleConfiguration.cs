namespace Betalish.Persistence.Configuration;

class NetworkRuleConfiguration : IEntityTypeConfiguration<NetworkRule>
{
    public void Configure(EntityTypeBuilder<NetworkRule> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Range)
            .HasMaxLength(MaxLengths.Common.Ip.Prefix.IPv6);

        builder.Property(p => p.BaseAddress2)
            .HasMaxLength(MaxLengths.Common.Ip.Prefix.IPv6);
    }
}
