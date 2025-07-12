using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Customers.EditCustomerPerson;

public class EditCustomerPersonCommandModel
{
    public int Id { get; set; }

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
    public string Name { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.Customer.EmailAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? EmailAddress { get; set; }
}
