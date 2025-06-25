namespace Betalish.Persistence.Configuration
{
    class SessionRecordConfiguration : IEntityTypeConfiguration<SessionRecord>
    {
        public void Configure(EntityTypeBuilder<SessionRecord> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.IpAddress)
                .IsRequired(false)
                .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);
        }
    }
}
