namespace Betalish.Persistence.Configuration;

class NetworkRuleConfiguration : IEntityTypeConfiguration<NetworkRule>
{
    public void Configure(EntityTypeBuilder<NetworkRule> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.BaseAddress)
            .HasMaxLength(MaxLengths.Common.Ip.Prefix.IPv6);
    }
}
