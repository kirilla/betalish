namespace Betalish.Domain.Entities;

public class InvoiceDraft : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public bool IsCredit { get; set; }

    public required string About { get; set; }

    // Dates
    public required DateOnly? InvoiceDate { get; set; }

    // Terms
    public required int? PaymentTermDays { get; set; }
    public required string? PaymentTerms { get; set; }
    
    // Summary
    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal Total { get; set; }
    public decimal TotalRounding { get; set; }

    // Customer identity
    public required string Customer_Name { get; set; }

    public required CustomerKind CustomerKind { get; set; }

    public required string? Customer_Ssn10 { get; set; }
    public required string? Customer_Orgnum { get; set; }

    // Customer address
    public required string? Customer_Address1 { get; set; }
    public required string? Customer_Address2 { get; set; }
    public required string Customer_ZipCode { get; set; }
    public required string Customer_City { get; set; }
    public required string? Customer_Country { get; set; }

    // Customer email
    public required string? Customer_Email { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public List<InvoiceDraftRow> InvoiceDraftRows { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(CustomerKind))
            throw new InvalidEnumException(nameof(CustomerKind));
    }
}
