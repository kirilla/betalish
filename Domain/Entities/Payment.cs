namespace Betalish.Domain.Entities;

public class Payment : 
    ICreatedDateTime, 
    IUpdatedDateTime, 
    IFormatOnSave, 
    IValidateOnSave
{
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly Date { get; set; }

    public required PaymentKind PaymentKind { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }

    public int? InvoiceNumber { get; set; }

    public int? PaymentAccountId { get; set; }
    public PaymentAccount? PaymentAccount { get; set; } = null!;

    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; } = null!;

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<PaymentAccountingRow> PaymentAccountingRows { get; set; } = [];

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (Amount == 0)
            throw new ValidateOnSaveException(
                $"Oväntat belopp: {Amount}.");

        if (!Enum.IsDefined(PaymentKind))
            throw new ValidateOnSaveException(
                $"Oväntad PaymentKind: {PaymentKind}.");

        if (!Enum.IsDefined(PaymentMethod))
            throw new ValidateOnSaveException(
                $"Oväntad PaymentMethod: {PaymentMethod}.");
    }
}
