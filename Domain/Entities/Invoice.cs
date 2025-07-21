namespace Betalish.Domain.Entities;

public class Invoice : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public InvoiceStatus InvoiceStatus { get; set; }

    public int? InvoiceNumber { get; set; }

    public bool IsCredit { get; set; }

    public required string About { get; set; }

    // Summary
    public required decimal NetAmount { get; set; }
    public required decimal VatAmount { get; set; }
    public required decimal Total { get; set; }
    public required decimal TotalRounding { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<InvoiceRow> InvoiceRows { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(InvoiceStatus))
            throw new ValidateOnSaveException(
                $"Undefined InvoiceStatus: {(int)InvoiceStatus}");

        if (InvoiceStatus == InvoiceStatus.Issued &&
            InvoiceNumber == null)
            throw new ValidateOnSaveException(
                "Issued invoice missing InvoiceNumber.");
    }
}
