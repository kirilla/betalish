using Betalish.Common.Constants;

namespace Betalish.Domain.Entities;

public class PaymentTerms : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required InvoiceKind InvoiceKind { get; set; }

    // Stages
    public required bool Reminder { get; set; }
    public required bool Demand { get; set; }
    public required bool Collect { get; set; }

    // Time frame
    public required int PaymentTermDays { get; set; }

    // Payment
    public required decimal? MinToConsiderPaid { get; set; }

    // Interest
    public required bool Interest { get; set; }

    // Fees
    public required decimal? ReminderFee { get; set; }
    public required decimal? DemandFee { get; set; }

    // Relations
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];

    public void FormatOnSave()
    {
        if (MinToConsiderPaid == 0)
            MinToConsiderPaid = null;

        if (ReminderFee == 0)
            ReminderFee = null;

        if (DemandFee == 0)
            DemandFee = null;

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
        if (!Enum.IsDefined(InvoiceKind))
            throw new InvalidEnumException(nameof(InvoiceKind));

        AssertInvoiceKindAllowed(InvoiceKind);

        if (MinToConsiderPaid < 0)
            throw new ValidateOnSaveException("MinToConsiderPaid");

        if (PaymentTermDays < Defaults.Invoice.PaymentTermDays.Min ||
            PaymentTermDays > Defaults.Invoice.PaymentTermDays.Max)
            throw new ValidateOnSaveException("PaymentTermDays");
    }

    public static void AssertInvoiceKindAllowed(InvoiceKind kind)
    {
        var allowedKinds = new List<InvoiceKind>()
        {
            InvoiceKind.Avi,
            InvoiceKind.Debit,
            InvoiceKind.Membership
        };

        if (!allowedKinds.Contains(kind))
            throw new Exception(
                $"PaymentTerms kan inte skapas för typen {kind}.");
    }
}
