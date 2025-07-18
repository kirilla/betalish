namespace Betalish.Domain.Entities;

public class Invoice : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required string About { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<InvoiceRow> InvoiceRows { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
