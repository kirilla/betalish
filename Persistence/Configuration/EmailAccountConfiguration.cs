namespace Betalish.Persistence.Configuration;

class EmailAccountConfiguration : IEntityTypeConfiguration<EmailAccount>
{
    public void Configure(EntityTypeBuilder<EmailAccount> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FromName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAccount.FromName);

        builder.Property(p => p.FromAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAccount.FromAddress);

        builder.Property(p => p.ReplyToName)
            .HasMaxLength(MaxLengths.Domain.EmailAccount.ReplyToName);

        builder.Property(p => p.ReplyToAddress)
            .HasMaxLength(MaxLengths.Domain.EmailAccount.ReplyToAddress);

        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAccount.Password);

        builder.Property(p => p.SmtpHost)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAccount.SmtpHost);
    }
}
