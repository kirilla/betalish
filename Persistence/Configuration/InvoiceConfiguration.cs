namespace Betalish.Persistence.Configuration;

class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasIndex(e => new { e.ClientId, e.InvoiceNumber })
            .IsUnique()
            .HasFilter("[InvoiceNumber] IS NOT NULL");

        builder
            .HasIndex(e => e.ClientId); // Make EF keep the FK index

        builder
            .HasIndex(e => e.CustomerId_Hint);
        // Not a FK. Merely a hint.
        // Customers come and go, but invoices are forever.
        // CustomerId is used in conjunction with CustomerGuid,
        // to ensure that it's still the same Customer.

        builder.Property(p => p.About)
            .HasMaxLength(MaxLengths.Common.Invoice.About);

        builder.Property(p => p.PaymentTerms)
            .HasMaxLength(MaxLengths.Common.Invoice.PaymentTerms);

        // Summary
        builder.Property(p => p.NetAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.VatAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Total)
            .HasPrecision(18, 2);

        builder.Property(p => p.TotalRounding)
            .HasPrecision(18, 2);

        // Payment
        builder.Property(p => p.Balance)
            .HasPrecision(18, 2);

        builder.Property(p => p.LeftToPay)
            .HasPrecision(18, 2);

        // Customer identity
        builder.Property(p => p.Customer_Name)
            .HasMaxLength(MaxLengths.Common.Person.FullName);

        builder.Property(p => p.Customer_Ssn10)
            .HasMaxLength(MaxLengths.Common.Ssn.Ssn10);

        builder.Property(p => p.Customer_Orgnum)
            .HasMaxLength(MaxLengths.Common.Organization.Orgnum);

        // Customer address
        builder.Property(p => p.Customer_Address1)
            .HasMaxLength(MaxLengths.Common.Address.Address1);

        builder.Property(p => p.Customer_Address2)
            .HasMaxLength(MaxLengths.Common.Address.Address2);

        builder.Property(p => p.Customer_City)
            .HasMaxLength(MaxLengths.Common.Address.City);

        builder.Property(p => p.Customer_Country)
            .HasMaxLength(MaxLengths.Common.Address.Country);

        builder.Property(p => p.Customer_ZipCode)
            .HasMaxLength(MaxLengths.Common.Address.ZipCode);

        // Customer email
        builder.Property(p => p.Customer_Email)
            .HasMaxLength(MaxLengths.Common.Email.Address);

        // Relations
        builder.HasMany(x => x.DistributionMessages)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);

        builder.HasMany(x => x.InvoiceAccountingRows)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);

        builder.HasMany(x => x.InvoiceFees)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);

        builder.HasMany(x => x.InvoiceRows)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);

        builder
            .HasMany(x => x.CreditBalanceRows)
            .WithOne(x => x.CreditInvoice)
            .HasForeignKey(x => x.CreditInvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.DebitBalanceRows)
            .WithOne(x => x.DebitInvoice)
            .HasForeignKey(x => x.DebitInvoiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.DraftBalanceRows)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId);

        builder
            .HasMany(x => x.Payments)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .IsRequired(false);

        builder
            .HasOne(x => x.InvoicePlan)
            .WithOne(x => x.Invoice)
            .HasForeignKey<InvoicePlan>(x => x.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // Ignore
        builder.Ignore(x => x.IsDebit);
    }
}
