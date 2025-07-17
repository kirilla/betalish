namespace Betalish.Domain.Entities;

public class InvoiceTemplateRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int InvoiceTemplateId { get; set; }
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
