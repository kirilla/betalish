namespace Betalish.Persistence.Configuration;

class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.Batch.Name);

        builder.HasMany(x => x.Invoices)
            .WithOne(x => x.Batch)
            .HasForeignKey(x => x.BatchId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.InvoiceDrafts)
            .WithOne(x => x.Batch)
            .HasForeignKey(x => x.BatchId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
