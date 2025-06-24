namespace Betalish.Persistence.Configuration;

class LogItemConfiguration : IEntityTypeConfiguration<LogItem>
{
    public void Configure(EntityTypeBuilder<LogItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.LogItem.Description);

        builder.Property(p => p.Exception)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.LogItem.Exception);

        builder.Property(p => p.InnerException)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.LogItem.InnerException);

        builder.Property(p => p.IpAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);
    }
}
