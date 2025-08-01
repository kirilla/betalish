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

        builder.HasMany(x => x.Articles)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.Batches)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ClientAuths)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEmailAccounts)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEmailMessages)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEvents)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.Customers)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Invoices)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.InvoiceRanges)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.InvoiceTemplates)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.LedgerAccounts)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.PaymentAccounts)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .IsRequired(false);

        builder.HasMany(x => x.SessionRecords)
            .WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId)
            .IsRequired(false);
    }
}
