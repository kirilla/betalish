namespace Betalish.Domain.Entities;

public class InvoiceDraft : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required InvoiceKind InvoiceKind { get; set; }

    public bool IsCredit => InvoiceKind == InvoiceKind.Credit;
    public bool IsDebit => InvoiceKind == InvoiceKind.Debit;
    public bool IsDebitLike => InvoiceKind != InvoiceKind.Credit;

    public required string About { get; set; }

    // Dates
    public required DateOnly? InvoiceDate { get; set; }
    
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

    // Hints
    public required int? CustomerId_Hint { get; set; }
    public required Guid? CustomerGuid { get; set; }

    // Relations
    public int? BatchId { get; set; }
    public Batch? Batch { get; set; } = null!;

    public int? PaymentTermsId { get; set; }
    public PaymentTerms? PaymentTerms { get; set; } = null!;

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    // Rows
    public List<InvoiceDraftRow> InvoiceDraftRows { get; set; } = [];

    // Balance
    public List<DraftBalanceRow> DraftBalanceRows { get; set; } = [];

    public void FormatOnSave()
    {
        Customer_Email = Customer_Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(Customer_Address1) &&
            !string.IsNullOrWhiteSpace(Customer_Address2))
        {
            Customer_Address1 = Customer_Address2;
            Customer_Address2 = null;
        }
    }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(CustomerKind))
            throw new InvalidEnumException(nameof(CustomerKind));

        if (!Enum.IsDefined(InvoiceKind))
            throw new InvalidEnumException(nameof(InvoiceKind));

        AssertInvoiceKindAllowed(InvoiceKind);

        if (IsDebitLike && PaymentTermsId == null)
            throw new ValidateOnSaveException(
                $"Debit draft {Id} should have PaymentTerms.");

        if (IsCredit && PaymentTermsId.HasValue)
            throw new ValidateOnSaveException(
                $"Credit draft {Id} should not have a PaymentTerms.");
    }

    public static void AssertInvoiceKindAllowed(InvoiceKind kind)
    {
        var allowedKinds = new List<InvoiceKind>()
        {
            InvoiceKind.Avi,
            InvoiceKind.Credit,
            InvoiceKind.Debit,
            InvoiceKind.InterestAndFees,
            InvoiceKind.Membership,
            InvoiceKind.Rest,
        };

        if (!allowedKinds.Contains(kind))
            throw new Exception(
                $"InvoiceDraft kan inte skapas för typen {kind}.");
    }
}
