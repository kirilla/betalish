namespace Betalish.Persistence.Configuration;

class EmailImageConfiguration : IEntityTypeConfiguration<EmailImage>
{
    public void Configure(EntityTypeBuilder<EmailImage> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailImage.Name);

        builder.Property(p => p.ContentType)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.EmailImage.ContentType);
    }
}
