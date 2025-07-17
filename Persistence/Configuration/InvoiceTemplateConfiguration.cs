namespace Betalish.Persistence.Configuration;

class InvoiceTemplateConfiguration : IEntityTypeConfiguration<InvoiceTemplate>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplate> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(x => x.InvoiceTemplateRows)
            .WithOne(x => x.InvoiceTemplate)
            .HasForeignKey(x => x.InvoiceTemplateId);
    }
}
