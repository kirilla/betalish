namespace Betalish.Domain.Entities;

public class InvoiceRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public bool IsCredit { get; set; }

    public int ArticleNumber { get; set; }

    public required string ArticleName { get; set; }

    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public required string Unit { get; set; }

    public decimal VatRate { get; set; }

    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public required string RevenueAccount { get; set; }
    public string? VatAccount { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void FormatOnSave()
    {
        NetAmount = GetNetAmount();
        VatAmount = GetVatAmount();
        TotalAmount = GetTotalAmount();
    }

    public void ValidateOnSave()
    {
        if (Quantity < 0)
            throw new ValidateOnSaveException();

        if (UnitPrice < 0)
            throw new ValidateOnSaveException();
    }

    private decimal GetNetAmount()
    {
        var amount = (Quantity * UnitPrice).RoundToEven();

        return IsCredit ? -amount : amount;
    }

    private decimal GetVatAmount()
    {
        return (GetNetAmount() * (VatRate / 100)).RoundToEven();
    }

    private decimal GetTotalAmount()
    {
        return GetNetAmount() + GetVatAmount();
    }
}
