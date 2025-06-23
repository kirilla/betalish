namespace Betalish.Persistence.Configuration;

class ClientEmailMessageConfiguration : IEntityTypeConfiguration<ClientEmailMessage>
{
    public void Configure(EntityTypeBuilder<ClientEmailMessage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ToName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.ToName);

        builder.Property(p => p.ToAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.ToAddress);

        builder.Property(p => p.FromName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.FromName);

        builder.Property(p => p.FromAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.FromAddress);

        builder.Property(p => p.ReplyToName)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.ReplyToName);

        builder.Property(p => p.ReplyToAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.ReplyToAddress);

        builder.Property(p => p.Subject)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.Subject);

        builder.Property(p => p.HtmlBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.HtmlBody);

        builder.Property(p => p.TextBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.ClientEmailMessage.TextBody);
    }
}
