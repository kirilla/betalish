using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Articles.EditArticleAccounting;

public class EditArticleAccountingCommandModel
{
    public int Id { get; set; }

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
