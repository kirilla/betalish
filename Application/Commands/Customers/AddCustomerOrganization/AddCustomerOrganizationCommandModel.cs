using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Customers.AddCustomerOrganization;

public class AddCustomerOrganizationCommandModel
{
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
    public string Name { get; set; } = string.Empty;

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.Customer.EmailAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? EmailAddress { get; set; }
}
