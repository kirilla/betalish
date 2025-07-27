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
}
