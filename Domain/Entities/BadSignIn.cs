namespace Betalish.Domain.Entities;

public class BadSignIn : ICreatedDateTime, IFormatOnSave
{
    public int Id { get; set; }

    public string? IpAddress { get; set; }

    public string? Name { get; set; }
    public string? Password { get; set; }

    public int? NameLength { get; set; }
    public int? PasswordLength { get; set; }

    public string? Exception { get; set; }
    public string? InnerException { get; set; }

    public DateTime? Created { get; set; }

    public void FormatOnSave()
    {
        NameLength = Name?.Length;
        PasswordLength = Password?.Length;

        Name = Name?.Truncate(MaxLengths.Domain.BadSignIn.Name);
        Password = Password?.Truncate(MaxLengths.Domain.BadSignIn.Password);

        Exception = Exception?
            .Truncate(MaxLengths.Domain.BadSignIn.Exception);

        InnerException = InnerException?
            .Truncate(MaxLengths.Domain.BadSignIn.InnerException);

        if (Exception.HasValue() &&
            Exception!.StartsWith("Exception of type 'Betalish.Common.Exceptions.") &&
            Exception!.EndsWith("' was thrown."))
        {
            Exception = Exception?.Replace("Exception of type 'Betalish.Common.Exceptions.", "");
            Exception = Exception?.Replace("' was thrown.", "");
        }
    }
}
