namespace Betalish.Domain.Entities;

public class BalanceRow : ICreatedDateTime, IUpdatedDateTime
{
    public int Id { get; set; }

    public required int DebitInvoiceNumber { get; set; }
    public required int CreditInvoiceNumber { get; set; }
    
    public required decimal Amount { get; set; }

    public required DateOnly Date { get; set; }

    public required bool PaymentsCreated { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int CreditInvoiceId { get; set; }
    public Invoice CreditInvoice { get; set; } = null!;

    public int DebitInvoiceId { get; set; }
    public Invoice DebitInvoice { get; set; } = null!;
}
