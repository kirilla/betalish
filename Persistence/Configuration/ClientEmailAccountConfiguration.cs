namespace Betalish.Persistence.Configuration;

class ClientEmailAccountConfiguration : IEntityTypeConfiguration<ClientEmailAccount>
{
    public void Configure(EntityTypeBuilder<ClientEmailAccount> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FromName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.FromName);

        builder.Property(p => p.FromAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.FromAddress);

        builder.Property(p => p.ReplyToName)
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.ReplyToName);

        builder.Property(p => p.ReplyToAddress)
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.ReplyToAddress);

        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.Password);

        builder.Property(p => p.SmtpHost)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailAccount.SmtpHost);
    }
}
