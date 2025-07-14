using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Account.RegisterAccount;

public class RegisterAccountCommand(
    IDatabaseService database,
    IOptions<SignUpConfiguration> options) : IRegisterAccountCommand
{
    private readonly SignUpConfiguration _config = options.Value;

    public async Task Execute(IUserToken userToken, RegisterAccountCommandModel model)
    {
        if (!_config.AllowRegisterAccount)
            throw new FeatureTurnedOffException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Ssn12 = model.Ssn12.StripNonNumeric();

        if (string.IsNullOrWhiteSpace(model.Ssn12) ||
            string.IsNullOrWhiteSpace(model.Name) ||
            string.IsNullOrWhiteSpace(model.EmailAddress) ||
            string.IsNullOrWhiteSpace(model.Password))
            throw new NotPermittedException();

        if (!SsnLogic.IsValidSsn12(model.Ssn12))
            throw new InvalidSsnException();

        model.EmailAddress = model.EmailAddress.Trim().ToLowerInvariant();

        if (await database.UserEmails.AnyAsync(x => x.Address == model.EmailAddress))
            throw new EmailAlreadyTakenException();

        var guid = Guid.NewGuid();

        if (await database.Users.AnyAsync(x => x.Guid == guid))
            throw new Exception("User.Guid collision.");

        var user = new User()
        {
            Name = model.Name,
            Guid = guid,
            PasswordHash = string.Empty,
            UserPurpose = UserPurpose.Client,
        };

        var hasher = new PasswordHasher<User>();

        var hash = hasher.HashPassword(user, model.Password);

        user.PasswordHash = hash;

        database.Users.Add(user);

        var ssn = new UserSsn()
        {
            Ssn12 = model.Ssn12.StripNonNumeric(),
            Ssn10 = model.Ssn12.ToSsn10(),
            SsnDate = model.Ssn12.ToDateOnly(),
            User = user,
        };

        database.UserSsns.Add(ssn);

        var emailAddress = new UserEmail()
        {
            User = user,
            Address = model.EmailAddress,
        };

        database.UserEmails.Add(emailAddress);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return _config.AllowRegisterAccount;
    }
}
