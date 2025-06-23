namespace Betalish.Persistence.Configuration;

class UserEmailConfiguration : IEntityTypeConfiguration<UserEmail>
{
    public void Configure(EntityTypeBuilder<UserEmail> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Email.Address);

        builder.HasIndex(p => p.Address).IsUnique();
    }
}
