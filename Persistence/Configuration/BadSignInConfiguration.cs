namespace Betalish.Persistence.Configuration;

class BadSignInConfiguration : IEntityTypeConfiguration<BadSignIn>
{
    public void Configure(EntityTypeBuilder<BadSignIn> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IpAddress)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Common.Ip.Address.IPv6);

        builder.Property(p => p.Name)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.BadSignIn.Name);

        builder.Property(p => p.Password)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.BadSignIn.Password);

        builder.Property(p => p.Exception)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.BadSignIn.Exception);

        builder.Property(p => p.InnerException)
            .IsRequired(false)
            .HasMaxLength(MaxLengths.Domain.BadSignIn.InnerException);
    }
}
