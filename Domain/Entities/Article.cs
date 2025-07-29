namespace Betalish.Domain.Entities;

public class Article : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int Number { get; set; }

    public required string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public required string UnitName { get; set; }

    public decimal VatRate { get; set; }

    public required string RevenueAccount { get; set; }
    public string? VatAccount { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<InvoiceTemplateRow> InvoiceTemplateRows { get; set; } = [];

    public void FormatOnSave()
    {
        if (VatRate == 0)
            VatAccount = null;

        // BUT: What about reverse VAT situations?
    }

    public void ValidateOnSave()
    {
        if (IsMissingRevenueAccount())
            throw new MissingRevenueAccountException();

        if (IsMissingVatAccount())
            throw new MissingVatAccountException();
    }

    public bool IsMissingRevenueAccount()
    {
        return
            RevenueAccount == null ||
            RevenueAccount == string.Empty ||
            RevenueAccount.Length != 4;
    }

    public bool IsMissingVatAccount()
    {
        return VatRate != 0 &&
            (VatAccount == null ||
            VatAccount == string.Empty ||
            VatAccount.Length != 4);
    }
}
