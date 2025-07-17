namespace Betalish.Domain.Entities;

public class InvoiceTemplateRow : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public decimal Quantity { get; set; }

    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;

    public int InvoiceTemplateId { get; set; }
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
