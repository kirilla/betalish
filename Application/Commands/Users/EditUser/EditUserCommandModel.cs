using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Users.EditUser;

public class EditUserCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(MaxLengths.Common.Person.FullName)]
    public string Name { get; set; } = string.Empty;

    public bool NoLogin { get; set; }
    public bool NoSave { get; set; }
}
