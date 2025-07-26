using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.PaymentAccounts.EditPaymentAccount;

public class EditPaymentAccountCommandModel
{
    public int Id { get; set; }

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
