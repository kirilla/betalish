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
    public string Name { get; set; } = string.Empty;

    // Address
    [StringLength(
        MaxLengths.Common.Address.Address1,
        ErrorMessage = "Skriv kortare.")]
    public string? Address1 { get; set; }

    [StringLength(
        MaxLengths.Common.Address.Address2,
        ErrorMessage = "Skriv kortare.")]
    public string? Address2 { get; set; }

    [Required(ErrorMessage = "Ange postnummer.")]
    [StringLength(
        MaxLengths.Common.Address.ZipCode,
        ErrorMessage = "Skriv kortare.")]
    public string? ZipCode { get; set; }

    [Required(ErrorMessage = "Ange postort.")]
    [StringLength(
        MaxLengths.Common.Address.City,
        ErrorMessage = "Skriv kortare.")]
    public string? City { get; set; }

    [StringLength(
        MaxLengths.Common.Address.Country,
        ErrorMessage = "Skriv kortare.")]
    public string? Country { get; set; }

    // Email
    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Domain.Customer.EmailAddress,
        ErrorMessage = "Skriv kortare.")]
    public string? EmailAddress { get; set; }
}
