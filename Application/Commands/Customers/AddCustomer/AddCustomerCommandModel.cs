using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Customers.AddCustomer;

public class AddCustomerCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Customer.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [Required(ErrorMessage = "Ange epostadress.")]
    [StringLength(
        MaxLengths.Domain.Customer.Address,
        ErrorMessage = "Skriv kortare.")]
    public string Address { get; set; }
}
