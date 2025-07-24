namespace Betalish.Domain.Entities;

public class BalanceRow : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public required int InvoiceNumber { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly Date { get; set; }

    public required bool PaymentsCreated { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int CreditInvoiceID { get; set; }
    public Invoice CreditInvoice { get; set; } = null!;

    public int DebetInvoiceID { get; set; }
    public Invoice DebetInvoice { get; set; } = null!;
}
