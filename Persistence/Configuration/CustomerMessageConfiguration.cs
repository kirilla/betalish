namespace Betalish.Persistence.Configuration;

class CustomerMessageConfiguration : IEntityTypeConfiguration<CustomerMessage>
{
    public void Configure(EntityTypeBuilder<CustomerMessage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ToName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.ToName);

        builder.Property(p => p.ToAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.ToAddress);

        builder.Property(p => p.FromName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.FromName);

        builder.Property(p => p.FromAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.FromAddress);

        builder.Property(p => p.ReplyToName)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.ReplyToName);

        builder.Property(p => p.ReplyToAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.ReplyToAddress);

        builder.Property(p => p.Subject)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.Subject);

        builder.Property(p => p.HtmlBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.HtmlBody);

        builder.Property(p => p.TextBody)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.CustomerMessage.TextBody);
    }
}
