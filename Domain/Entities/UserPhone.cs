namespace Betalish.Domain.Entities;

public class UserPhone :
    ICreatedDateTime, IUpdatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public string Number { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public void FormatOnSave()
    {
        Number = Number.Trim().StripNonPhoneNumberChars();

        this.SetEmptyStringsToNull();
    }
}
