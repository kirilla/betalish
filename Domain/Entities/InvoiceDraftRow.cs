namespace Betalish.Domain.Entities;

public class InvoiceDraftRow : IFormatOnSave, IValidateOnSave
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

    public int InvoiceDraftId { get; set; }
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public void FormatOnSave()
    {
        NetAmount = _NetAmount;
        VatAmount = _VatAmount;
        TotalAmount = _TotalAmount;

        if (VatRate == 0)
            VatAccount = null;
    }

    public void ValidateOnSave()
    {
        if (Quantity < 0)
            throw new ValidateOnSaveException();

        if (UnitPrice < 0)
            throw new ValidateOnSaveException();

        if (IsMissingRevenueAccount())
            throw new MissingRevenueAccountException();

        if (IsMissingVatAccount())
            throw new MissingVatAccountException();
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
            return (_NetAmount * (VatRate / 100)).RoundToEven();
        }
    }

    private decimal _TotalAmount
    {
        get
        {
            return _NetAmount + _VatAmount;
        }
    }

    private bool IsMissingRevenueAccount()
    {
        return RevenueAccount.AccountIsValid() == false;
    }

    private bool IsMissingVatAccount()
    {
        return
            VatRate != 0 &&
            VatAccount.AccountIsValid() == false;
    }
}
