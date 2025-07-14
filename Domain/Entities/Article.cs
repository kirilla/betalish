namespace Betalish.Domain.Entities;

public class Article : IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public ArticleKind ArticleKind { get; set; }

    public int Number { get; set; }

    public required string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal VatValue { get; set; }

    public required string UnitName { get; set; }

    public required string Account { get; set; }
    public required string VatAccount { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void FormatOnSave()
    {
    }

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(ArticleKind))
            throw new InvalidEnumException();
    }
}
