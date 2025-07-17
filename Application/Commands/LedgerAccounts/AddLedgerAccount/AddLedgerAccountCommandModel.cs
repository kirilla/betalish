using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.LedgerAccounts.AddLedgerAccount;

public class AddLedgerAccountCommandModel
{
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
