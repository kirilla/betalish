namespace Betalish.Domain.Entities;

public class PaymentAccount : IValidateOnSave
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string? Description { get; set; }

    public required string Account { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<Payment> Payments { get; set; } = [];

    public void ValidateOnSave()
    {
        if (!Account.AccountIsValid())
            throw new MissingAccountException();
    }
}
