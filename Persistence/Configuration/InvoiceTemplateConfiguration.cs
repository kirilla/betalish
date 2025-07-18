namespace Betalish.Persistence.Configuration;

class InvoiceTemplateConfiguration : IEntityTypeConfiguration<InvoiceTemplate>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplate> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(MaxLengths.Domain.InvoiceTemplate.Name);

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        builder.HasMany(x => x.InvoiceTemplateRows)
            .WithOne(x => x.InvoiceTemplate)
            .HasForeignKey(x => x.InvoiceTemplateId);
    }
}
