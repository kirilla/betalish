using Betalish.Common.Logic;

namespace Betalish.Domain.Entities;

public class UserEmail : 
    ICreatedDateTime, IUpdatedDateTime, IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public string Address { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public void FormatOnSave()
    {
        Address = Address.Trim().ToLowerInvariant();

        this.SetEmptyStringsToNull();
    }

    public void ValidateOnSave()
    {
        if (string.IsNullOrWhiteSpace(Address))
            throw new MissingEmailException();

        if (!RegexLogic.IsMatch(Address, Pattern.Common.Email.Address))
            throw new InvalidEmailException();
    }
}
