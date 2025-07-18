namespace Betalish.Domain.Entities;

public class InvoiceTemplate : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string About { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<InvoiceTemplateRow> InvoiceTemplateRows { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
