namespace Betalish.Domain.Entities;

public class BadSignIn : 
    ICreatedDateTime, IFormatOnSave, IValidateOnSave
{
    public int Id { get; set; }

    public SignInBy? SignInBy { get; set; }

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
        Name = Name?[..MaxLengths.Domain.BadSignIn.Name];
        Password = Password?[..MaxLengths.Domain.BadSignIn.Password];
    }

    public void ValidateOnSave()
    {
        if (SignInBy.HasValue &&
            !Enum.IsDefined(SignInBy.Value))
            throw new InvalidEnumException();
    }
}
