namespace Betalish.Domain.Entities;

public class CustomerTag : IFormatOnSave
{
    public int Id { get; set; }

    public required string Key { get; set; }

    public string? Value { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public void FormatOnSave()
    {
        Key = Key.Trim();

        Value = Value?.Trim();

        if (string.IsNullOrWhiteSpace(Value))
            Value = null;
    }
}
