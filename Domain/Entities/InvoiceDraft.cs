namespace Betalish.Domain.Entities;

public class InvoiceDraft : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public bool IsCredit { get; set; }

    public required string About { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public List<InvoiceDraftRow> InvoiceDraftRows { get; set; } = [];

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
