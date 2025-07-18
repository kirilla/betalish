namespace Betalish.Persistence.Configuration;

class CustomerTagConfiguration : IEntityTypeConfiguration<CustomerTag>
{
    public void Configure(EntityTypeBuilder<CustomerTag> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Key)
            .HasMaxLength(MaxLengths.Common.Tag.Key);

        builder.Property(p => p.Value)
            .HasMaxLength(MaxLengths.Common.Tag.Value);
    }
}
