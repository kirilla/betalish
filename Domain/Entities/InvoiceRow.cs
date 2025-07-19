namespace Betalish.Domain.Entities;

public class InvoiceRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int ArticleNumber { get; set; }

    public required string ArticleName { get; set; }

    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public required string Unit { get; set; }

    public bool IsCredit { get; set; }

    public decimal VatPercentage { get; set; }

    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void FormatOnSave()
    {
        NetAmount = _NetAmount;
        VatAmount = _VatAmount;
        TotalAmount = _TotalAmount;
    }

    public void ValidateOnSave()
    {
    }

    private decimal _NetAmount
    {
        get
        {
            var amount = (Quantity * UnitPrice).RoundToEven();

            return IsCredit ? -amount : amount;
        }
    }

    private decimal _VatAmount
    {
        get
        {
            return (_NetAmount * (VatPercentage / 100)).RoundToEven();
        }
    }

    private decimal _TotalAmount
    {
        get
        {
            return _NetAmount + _VatAmount;
        }
    }
}
