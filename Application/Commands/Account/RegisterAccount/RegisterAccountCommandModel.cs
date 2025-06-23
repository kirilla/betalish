using Betalish.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Account.RegisterAccount;

public class RegisterAccountCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [StringLength(MaxLengths.Common.Person.Name)]
    public string Name { get; set; }

    [RegularExpression(Pattern.Common.Email.Address)]
    [StringLength(MaxLengths.Common.Email.Address)]
    public string EmailAddress { get; set; }

    [StringLength(MaxLengths.Common.Password.Clear)]
    public string Password { get; set; }
}
