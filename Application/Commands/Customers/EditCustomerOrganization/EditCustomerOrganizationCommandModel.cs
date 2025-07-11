using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Customers.EditCustomerOrganization;

public class EditCustomerOrganizationCommandModel
{
    public int Id { get; set; }

    [RegularExpression(
        Pattern.Common.Organization.OrgnumPermissive,
        ErrorMessage = "10 siffror")]
    [StringLength(
        MaxLengths.Common.Organization.OrgnumPermissive,
        ErrorMessage = "10 siffror")]
    public string? Orgnum { get; set; }

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
