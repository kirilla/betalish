namespace Betalish.Domain.Entities;

public class Invoice : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
    }
}
