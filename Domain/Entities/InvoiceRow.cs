namespace Betalish.Domain.Entities;

public class InvoiceRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
