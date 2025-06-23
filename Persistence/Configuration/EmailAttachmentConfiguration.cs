namespace Betalish.Persistence.Configuration;

class EmailAttachmentConfiguration : IEntityTypeConfiguration<EmailAttachment>
{
    public void Configure(EntityTypeBuilder<EmailAttachment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAttachment.Name);

        builder.Property(p => p.ContentType)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailAttachment.ContentType);
    }
}
