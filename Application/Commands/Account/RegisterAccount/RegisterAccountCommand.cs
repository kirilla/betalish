using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Account.RegisterAccount;

public class RegisterAccountCommand(
    IDatabaseService database,
    IOptions<AccountConfiguration> accountOptions) : IRegisterAccountCommand
{
    private readonly AccountConfiguration _config = accountOptions.Value;

    public async Task Execute(IUserToken userToken, RegisterAccountCommandModel model)
    {
        if (!_config.RegisterAccountAllowed)
            throw new FeatureTurnedOffException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Ssn12 = model.Ssn12?.StripNonNumeric();

        if (string.IsNullOrWhiteSpace(model.Ssn12) ||
            string.IsNullOrWhiteSpace(model.Name) ||
            string.IsNullOrWhiteSpace(model.EmailAddress) ||
            string.IsNullOrWhiteSpace(model.Password))
            throw new NotPermittedException();

        if (!SsnService.IsValidSsn(model.Ssn12))
            throw new InvalidSsnException();

        model.EmailAddress = model.EmailAddress.Trim().ToLowerInvariant();

        if (await database.UserEmails.AnyAsync(x => x.Address == model.EmailAddress))
            throw new EmailAlreadyTakenException();

        var user = new User()
        {
            Ssn12 = model.Ssn12,
            Name = model.Name,
            Guid = Guid.NewGuid(),
        };

        if (await database.Users.AnyAsync(x => x.Guid == user.Guid))
            throw new Exception("User.Guid collision.");

        var hasher = new PasswordHasher<User>();

        var hash = hasher.HashPassword(user, model.Password);

        user.PasswordHash = hash;

        database.Users.Add(user);

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
        return _config.RegisterAccountAllowed;
    }
}
