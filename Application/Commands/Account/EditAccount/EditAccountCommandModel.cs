using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Account.EditAccount;

public class EditAccountCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Skriv ditt namn.")]
    [StringLength(MaxLengths.Common.Person.Name)]
    public string Name { get; set; }
}
