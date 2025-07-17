namespace Betalish.Domain.Entities;

public class LedgerAccount : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public required string Account { get; set; }
    public required string Description { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
