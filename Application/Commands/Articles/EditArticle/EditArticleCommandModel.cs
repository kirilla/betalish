using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Articles.EditArticle;

public class EditArticleCommandModel
{
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    [Required(ErrorMessage = "Ange artikelnummer.")]
    public int? Number { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Article.Name,
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }

    [RegularExpression(
        Pattern.Common.Decimal.Signed.TwoOptionalDecimals,
        ErrorMessage = "Ange pris med siffror")]
    [Required(ErrorMessage = "Ange pris.")]
    public string? UnitPrice { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange enhet.")]
    [StringLength(
        MaxLengths.Domain.Article.UnitName,
        ErrorMessage = "Skriv kortare.")]
    public string? UnitName { get; set; }

    [RegularExpression(
        Pattern.Common.Percentage.Unsigned.Percent,
        ErrorMessage = "Ange momssats med siffror")]
    [Required(ErrorMessage = "Ange momssats.")]
    [StringLength(MaxLengths.Common.Percent.ShortWithDecimals)]
    public string? VatRate { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Intäktskonto anges med fyra siffror")]
    [Required(ErrorMessage = "Ange intäktskonto.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? RevenueAccount { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Moms-konto anges med fyra siffror")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? VatAccount { get; set; }
}
