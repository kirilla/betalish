namespace Betalish.Domain.Entities;

public class Invoice : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public InvoiceStatus InvoiceStatus { get; set; }

    public int? InvoiceNumber { get; set; }

    public bool IsCredit { get; set; }
    public bool IsDebit => !IsCredit;

    public required string About { get; set; }

    // Dates
    public required DateOnly InvoiceDate { get; set; }
    public required DateOnly? DueDate { get; set; }

    public required DateOnly? ReminderDate { get; set; }
    public required DateOnly? ReminderDueDate { get; set; }

    public required DateOnly? DemandDate { get; set; }
    public required DateOnly? DemandDueDate { get; set; }

    public required DateOnly? CollectDate { get; set; }

    // Terms
    public required int? PaymentTermDays { get; set; }
    public required string? PaymentTerms { get; set; }

    // Summary
    public required decimal NetAmount { get; set; }
    public required decimal VatAmount { get; set; }
    public required decimal Total { get; set; }
    public required decimal TotalRounding { get; set; }

    // Payment
    public required decimal Balance { get; set; }
    public required decimal LeftToPay { get; set; }

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

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<DistributionTrigger> DistributionTriggers { get; set; } = [];

    public List<InvoiceFee> InvoiceFees { get; set; } = [];
    public List<InvoiceRow> InvoiceRows { get; set; } = [];

    public List<InvoiceAccountingRow> InvoiceAccountingRows { get; set; } = [];

    public List<BalanceRow> CreditBalanceRows { get; set; } = [];
    public List<BalanceRow> DebitBalanceRows { get; set; } = [];

    public List<DraftBalanceRow> DraftBalanceRows { get; set; } = [];

    public List<Payment> Payments { get; set; } = [];

    public InvoicePlan? InvoicePlan { get; set; }

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(CustomerKind))
            throw new InvalidEnumException(nameof(CustomerKind));

        if (!Enum.IsDefined(InvoiceStatus))
            throw new InvalidEnumException(nameof(InvoiceStatus));

        if (InvoiceStatus == InvoiceStatus.Issued &&
            InvoiceNumber == null)
            throw new ValidateOnSaveException(
                "Issued invoice missing InvoiceNumber.");

        if (InvoiceNumber.HasValue &&
            InvoiceNumber < 0)
            throw new ValidateOnSaveException(
                $"Negative InvoiceNumber: {InvoiceNumber}.");
    }
}
