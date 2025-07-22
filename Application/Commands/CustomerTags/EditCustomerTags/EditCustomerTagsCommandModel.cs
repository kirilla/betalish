using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.CustomerTags.EditCustomerTags;

public class EditCustomerTagsCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.AnythingMultiLine)]
    [StringLength(
        MaxLengths.Common.Tag.Multitude,
        ErrorMessage = "Skriv kortare.")]
    public string? Tags { get; set; }
}
