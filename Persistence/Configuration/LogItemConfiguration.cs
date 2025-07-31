namespace Betalish.Persistence.Configuration;

class LogItemConfiguration : IEntityTypeConfiguration<LogItem>
{
    public void Configure(EntityTypeBuilder<LogItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .HasMaxLength(MaxLengths.Domain.LogItem.Description);

        builder.Property(p => p.Exception)
            .HasMaxLength(MaxLengths.Domain.LogItem.Exception);

        builder.Property(p => p.InnerException)
            .HasMaxLength(MaxLengths.Domain.LogItem.InnerException);

        builder.Property(p => p.IpAddress)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);

        builder.Property(p => p.Source)
            .HasMaxLength(MaxLengths.Domain.LogItem.Source);
    }
}
