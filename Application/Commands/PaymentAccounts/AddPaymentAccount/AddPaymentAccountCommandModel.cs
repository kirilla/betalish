using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.PaymentAccounts.AddPaymentAccount;

public class AddPaymentAccountCommandModel
{
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.PaymentAccount.Name,
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }

    [StringLength(
        MaxLengths.Domain.PaymentAccount.Description, 
        ErrorMessage = "Skriv kortare.")]
    public string? Description { get; set; }

    [RegularExpression(
        Pattern.Common.Bookkeeping.Account,
        ErrorMessage = "Bokföringskonto anges med fyra siffror")]
    [Required(ErrorMessage = "Ange bokföringskonto.")]
    [StringLength(
        MaxLengths.Common.Bookkeeping.Account,
        ErrorMessage = "Skriv kortare.")]
    public string? Account { get; set; }
}
