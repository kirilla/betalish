namespace Betalish.Persistence.Configuration;

class UserSsnConfiguration : IEntityTypeConfiguration<UserSsn>
{
    public void Configure(EntityTypeBuilder<UserSsn> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Ssn12)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Ssn.Ssn12);

        builder.Property(p => p.Ssn10)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Ssn.Ssn10);

        builder.HasIndex(x => x.Ssn10)
            .IsUnique();
    }
}
