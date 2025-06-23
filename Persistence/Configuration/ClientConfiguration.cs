namespace Betalish.Persistence.Configuration;

class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Client.Name);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(MaxLengths.Domain.Client.Address);

        builder.HasMany(x => x.ClientAuths)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEmailAccounts)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEmailMessages)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.Customers)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);
    }
}
