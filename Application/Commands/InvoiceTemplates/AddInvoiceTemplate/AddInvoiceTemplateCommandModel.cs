using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceTemplates.AddInvoiceTemplate;

public class AddInvoiceTemplateCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.InvoiceTemplate.Name,
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange sammanfattning.")]
    [StringLength(
        MaxLengths.Common.Invoice.About,
        ErrorMessage = "Skriv kortare.")]
    public string? About { get; set; }
}
