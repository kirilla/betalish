namespace Betalish.Persistence.Configuration
{
    class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(MaxLengths.Common.Phone.Number);

            builder.HasIndex(p => p.Number).IsUnique();
        }
    }
}
