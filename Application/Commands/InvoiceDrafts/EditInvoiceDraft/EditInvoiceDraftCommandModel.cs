using Betalish.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

public class EditInvoiceDraftCommandModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange sammanfattning.")]
    [StringLength(
        MaxLengths.Common.Invoice.About,
        ErrorMessage = "Skriv kortare.")]
    public string? About { get; set; }

    // Dates
    public string? InvoiceDate { get; set; }

    // Customer address
    [StringLength(
        MaxLengths.Common.Address.Address1,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_Address1 { get; set; }

    [StringLength(
        MaxLengths.Common.Address.Address2,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_Address2 { get; set; }

    [Required(ErrorMessage = "Ange postnummer.")]
    [StringLength(
        MaxLengths.Common.Address.ZipCode,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_ZipCode { get; set; }

    [Required(ErrorMessage = "Ange postort.")]
    [StringLength(
        MaxLengths.Common.Address.City,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_City { get; set; }

    [StringLength(
        MaxLengths.Common.Address.Country,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_Country { get; set; }

    // Customer email
    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(
        MaxLengths.Common.Email.Address,
        ErrorMessage = "Skriv kortare.")]
    public string? Customer_Email { get; set; }

    // Strategy
    public bool IsDebit { get; set; }

    [RequiredIfBoolean(nameof(IsDebit), true, "Ange betalvillkor.")]
    public int? PaymentTermsId { get; set; }
}
