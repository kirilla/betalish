namespace Betalish.Persistence.Configuration;

class EmailMessageConfiguration : IEntityTypeConfiguration<EmailMessage>
{
    public void Configure(EntityTypeBuilder<EmailMessage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ToName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.ToName);

        builder.Property(p => p.ToAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.ToAddress);

        builder.Property(p => p.FromName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.FromName);

        builder.Property(p => p.FromAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.FromAddress);

        builder.Property(p => p.ReplyToName)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.EmailMessage.ReplyToName);

        builder.Property(p => p.ReplyToAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.EmailMessage.ReplyToAddress);

        builder.Property(p => p.Subject)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.Subject);

        builder.Property(p => p.HtmlBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.HtmlBody);

        builder.Property(p => p.TextBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailMessage.TextBody);

        builder.HasMany(x => x.EmailAttachments)
            .WithOne(x => x.EmailMessage)
            .HasForeignKey(x => x.EmailMessageId);

        builder.HasMany(x => x.EmailImages)
            .WithOne(x => x.EmailMessage)
            .HasForeignKey(x => x.EmailMessageId);
    }
}
