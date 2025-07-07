namespace Betalish.Persistence.Configuration;

class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
{
    public void Configure(EntityTypeBuilder<UserEvent> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.UserEvent.Description);

        builder.Property(p => p.IpAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);
    }
}
