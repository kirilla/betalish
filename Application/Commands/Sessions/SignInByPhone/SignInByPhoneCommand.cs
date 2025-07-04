using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Sessions.SignInByPhone;

public class SignInByPhoneCommand(
    IDatabaseService database,
    IOptions<SignInConfiguration> options) : ISignInByPhoneCommand
{
    private readonly SignInConfiguration _config = options.Value;

    public async Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInByPhoneCommandModel model, string? ipAddress)
    {
        if (!_config.AllowSignInByPhone)
            throw new FeatureTurnedOffException();

        if (userToken.IsAuthenticated)
            throw new AlreadyLoggedInException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.PhoneNumber = model.PhoneNumber.Trim().StripNonPhoneNumberChars();

        if (string.IsNullOrWhiteSpace(model.PhoneNumber))
            throw new NotPermittedException();

        var phone = await database.UserPhones
            .Where(x => x.Number == model.PhoneNumber)
            .SingleOrDefaultAsync() ??
            throw new UserNotFoundException();

        var user = await database.Users
            .Where(x => x.Id == phone.UserId)
            .SingleOrDefaultAsync() ??
            throw new UserNotFoundException();

        if (user.NoLogin)
            throw new UserNoLoginException();

        var hasher = new PasswordHasher<User>();

        var result = hasher.VerifyHashedPassword(
            user, user.PasswordHash, model.Password);

        switch (result)
        {
            case PasswordVerificationResult.Success:
                // continue
                break;

            case PasswordVerificationResult.SuccessRehashNeeded:
                user.PasswordHash = hasher.HashPassword(user, model.Password);
                // continue
                break;

            case PasswordVerificationResult.Failed:
                throw new PasswordVerificationFailedException();

            default:
                throw new Exception("Unknown password verification result.");
        }

        var session = new Session()
        {
            UserId = user.Id,
            Guid = Guid.NewGuid(),
            IpAddress = ipAddress,
            SignInBy = SignInBy.Phone,
        };

        database.Sessions.Add(session);

        userToken.NoSave = false;

        await database.SaveAsync(userToken);

        return new SessionGuidResultModel()
        {
            UserId = user.Id,
            UserGuid = user.Guid!.Value,
            SessionGuid = session.Guid!.Value,
        };
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return _config.AllowSignInByPhone;
    }
}
