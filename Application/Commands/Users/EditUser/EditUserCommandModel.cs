using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Users.EditUser;

public class EditUserCommandModel
{
    public int Id { get; set; }

    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(MaxLengths.Common.Person.Name)]
    public string Name { get; set; }
}
