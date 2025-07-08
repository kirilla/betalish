using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Sessions.SignInByEmail;

public class SignInByEmailCommand(
    IDatabaseService database,
    IOptions<SignInConfiguration> options) : ISignInByEmailCommand
{
    private readonly SignInConfiguration _config = options.Value;

    public async Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInByEmailCommandModel model, string? ipAddress)
    {
        if (!_config.AllowSignInByEmail)
            throw new FeatureTurnedOffException();

        if (userToken.IsAuthenticated)
            throw new AlreadyLoggedInException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.EmailAddress = model.EmailAddress.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(model.EmailAddress))
            throw new NotPermittedException();

        var email = await database.UserEmails
            .Where(x => x.Address == model.EmailAddress)
            .SingleOrDefaultAsync() ??
            throw new UserNotFoundException();

        var user = await database.Users
            .Where(x => x.Id == email.UserId)
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
            SignInBy = SignInBy.Email,
        };

        database.Sessions.Add(session);

        userToken.NoSave = false;

        var userEvent = new UserEvent()
        {
            UserId = user.Id,
            UserEventKind = UserEventKind.SignInByEmail,
            IpAddress = ipAddress,
        };

        database.UserEvents.Add(userEvent);

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
        return _config.AllowSignInByEmail;
    }
}
