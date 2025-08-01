namespace Betalish.Application.Reports.UnpaidReports;

public class UnpaidInvoice
{
    public int Id { get; set; }

    public int? InvoiceNumber { get; set; }

    public bool IsCredit { get; set; }

    public required string About { get; set; }

    // Dates
    public required DateOnly InvoiceDate { get; set; }

    // Summary
    public required decimal Total { get; set; }

    // Payment
    public required decimal Balance { get; set; }
    public required decimal LeftToPay { get; set; }

    // Customer identity
    public required string Customer_Name { get; set; }

    // Relations
    public List<InvoiceFee> InvoiceFees { get; set; } = [];

    public List<BalanceRow> DebitBalanceRows { get; set; } = [];
    public List<BalanceRow> CreditBalanceRows { get; set; } = [];
    
    public List<Domain.Entities.Payment> Payments { get; set; } = [];

    // TODO: Interest?

    public void UpdatePaymentStatus()
    {
        Balance =
            (Total +
            InvoiceFees.Sum(x => x.Amount) -
            Payments.Sum(x => x.Amount));

        if (IsCredit)
        {
            Balance += CreditBalanceRows.Sum(x => x.Amount);
        }
        else
        {
            Balance -= DebitBalanceRows.Sum(x => x.Amount);
        }

        LeftToPay = decimal.Clamp(Balance, 0, decimal.MaxValue);
    }
}
