namespace Betalish.Domain.Entities;

public class InvoiceRange : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int StartNumber { get; set; }
    public int EndNumber { get; set; }

    public string? Comment { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public DateOnly EffectiveStartDate { get; set; }
    public DateOnly EffectiveEndDate { get; set; }

    // NOTE: The effective dates simplify queries,
    // which can be formulated as a simple closed range,
    // without cases for null values (open-ended ranges).

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void FormatOnSave()
    {
        EffectiveStartDate = StartDate ?? DateOnly.MinValue;
        EffectiveEndDate = EndDate ?? DateOnly.MaxValue;
    }

    public void ValidateOnSave()
    {
        if (EndNumber <= StartNumber)
            throw new ValidateOnSaveException(
                $"Ogiltigt intervall: {StartNumber}-{EndNumber}.");

        if (StartNumber < Ranges.Invoice.Number.Min ||
            StartNumber > Ranges.Invoice.Number.Max)
            throw new ValidateOnSaveException(
                $"Första fakturanummer måste vara mellan " +
                $"{Ranges.Invoice.Number.Min} och " +
                $"{Ranges.Invoice.Number.Max}");

        if (EndNumber < Ranges.Invoice.Number.Min ||
            EndNumber > Ranges.Invoice.Number.Max)
            throw new ValidateOnSaveException(
                $"Sista fakturanummer måste vara mellan " +
                $"{Ranges.Invoice.Number.Min} och " +
                $"{Ranges.Invoice.Number.Max}");
    }

    public bool ContainsDate(DateOnly date)
    {
        return
            date >= EffectiveStartDate &&
            date <= EffectiveEndDate;
    }

    public bool ContainsNumber(int number)
    {
        return
            number >= StartNumber &&
            number <= EndNumber;
    }
}
