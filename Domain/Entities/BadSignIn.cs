namespace Betalish.Domain.Entities;

public class BadSignIn : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public string? IpAddress { get; set; }

    public string? Name { get; set; }
    public string? Password { get; set; }

    public int? NameLength { get; set; }
    public int? PasswordLength { get; set; }

    public bool BadUsername { get; set; }
    public bool BadPassword { get; set; }
    public bool OtherException { get; set; }

    public DateTime? Created { get; set; }

    public void FormatOnSave()
    {
        NameLength = Name?.Length;
        PasswordLength = Password?.Length;

        Name = Name?.Truncate(MaxLengths.Domain.BadSignIn.Name);
        Password = Password?.Truncate(MaxLengths.Domain.BadSignIn.Password);
    }
}
