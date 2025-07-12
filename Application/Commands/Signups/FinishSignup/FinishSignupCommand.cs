using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Signups.FinishSignup;

public class FinishSignupCommand(
    IDatabaseService database,
    IOptions<SignUpConfiguration> options) : IFinishSignupCommand
{
    private readonly SignUpConfiguration _config = options.Value;

    public async Task Execute(IUserToken userToken, FinishSignupCommandModel model)
    {
        if (!_config.AllowFinishSignup)
            throw new FeatureTurnedOffException();

        if (userToken.IsAuthenticated)
            throw new PleaseLogOutException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (model.Guid == null ||
            model.Guid == Guid.Empty)
            throw new InvalidDataException();

        var signup = await database.Signups
            .Where(x => x.Guid == model.Guid)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        // Create User
        var ssn10 = signup.Ssn12.ToSsn10();

        if (await database.UserSsns.AnyAsync(x => x.Ssn10 == ssn10))
            throw new BlockedBySsnException();

        if (await database.UserEmails.AnyAsync(x => x.Address == signup.EmailAddress))
            throw new BlockedByEmailException();

        var guid = Guid.NewGuid();

        if (await database.Users.AnyAsync(x => x.Guid == guid))
            throw new BlockedByGuidException();

        var user = new User()
        {
            Guid = guid,
            Name = signup.PersonName,
        };

        var hasher = new PasswordHasher<User>();

        var hash = hasher.HashPassword(user, model.Password);

        user.PasswordHash = hash;

        database.Users.Add(user);

        var ssn = new UserSsn()
        {
            Ssn12 = signup.Ssn12,
            User = user,
        };

        database.UserSsns.Add(ssn);

        var email = new UserEmail()
        {
            User = user,
            Address = signup.EmailAddress,
        };

        database.UserEmails.Add(email);

        var phone = new UserPhone()
        {
            User = user,
            Number = signup.PhoneNumber,
        };

        database.UserPhones.Add(phone);

        database.Signups.Remove(signup);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return !userToken.IsAuthenticated; 
        
        // Disallow for logged in users.
    }
}
