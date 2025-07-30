namespace Betalish.Application.Reports.Payment;

public class PaymentDateSum
{
    public DateOnly Date { get; set; }

    public required string PaymentAccountName { get; set; }
    public required string BookkeepingAccount { get; set; }

    public decimal Amount { get; set; }

    public int Count { get; set; }
}
