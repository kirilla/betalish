using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.LedgerAccounts.EditLedgerAccount;

public class EditLedgerAccountCommandModel
{
    public int Id { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Bokföringskonto anges med fyra siffror")]
    [Required(ErrorMessage = "Ange bokföringskonto.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? Account { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Description,
        ErrorMessage = "Skriv kortare.")]
    public string? Description { get; set; }
}
