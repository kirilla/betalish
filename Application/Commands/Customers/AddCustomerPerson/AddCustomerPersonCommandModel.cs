using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Customers.AddCustomerPerson;

public class AddCustomerPersonCommandModel
{
    [RegularExpression(
        Pattern.Common.Ssn.Ssn10Permissive,
        ErrorMessage = "10 siffror")]
    [StringLength(
        MaxLengths.Common.Ssn.Ssn10Permissive,
        ErrorMessage = "10 siffror")]
    public string? Ssn10 { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Customer.Name,
        ErrorMessage = "Skriv kortare.")]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.Customer.EmailAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? EmailAddress { get; set; }
}
