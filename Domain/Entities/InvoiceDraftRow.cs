namespace Betalish.Domain.Entities;

public class InvoiceDraftRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int InvoiceDraftId { get; set; }
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
