using Betalish.Common.Constants;

namespace Betalish.Domain.Entities;

public class BillingStrategy : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required bool Interest { get; set; }
    public required bool Reminder { get; set; }
    public required bool Demand { get; set; }
    public required bool Collect { get; set; }

    public required int PaymentTermDays { get; set; }

    public required decimal? MinToConsiderPaid { get; set; }

    // Relations
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void FormatOnSave()
    {
        if (MinToConsiderPaid == 0)
            MinToConsiderPaid = null;

        if (Demand)
        {
            Reminder = true;
        }

        if (Collect)
        {
            Reminder = true;
            Demand = true;
        }
    }

    public void ValidateOnSave()
    {
        if (MinToConsiderPaid < 0)
            throw new ValidateOnSaveException("MinToConsiderPaid");

        if (PaymentTermDays < Defaults.Invoice.PaymentTermDays.Min ||
            PaymentTermDays > Defaults.Invoice.PaymentTermDays.Max)
            throw new ValidateOnSaveException("PaymentTermDays");
    }
}
