using Betalish.Common.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static Betalish.Common.Validation.MaxLengths.Common;

namespace Betalish.Application.Commands.Sessions.SignInBySsn;

public class SignInBySsnCommand(
    IDatabaseService database,
    IOptions<SignInConfiguration> options) : ISignInBySsnCommand
{
    private readonly SignInConfiguration _config = options.Value;

    public async Task<SessionGuidResultModel> Execute(
        IUserToken userToken, SignInBySsnCommandModel model, string? ipAddress)
    {
        if (!_config.AllowSignInBySsn)
            throw new FeatureTurnedOffException();

        if (userToken.IsAuthenticated)
            throw new AlreadyLoggedInException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Ssn12 = model.Ssn12?.StripNonNumeric();

        if (string.IsNullOrWhiteSpace(model.Ssn12))
            throw new NotPermittedException();

        if (model.Ssn12.Length != 12)
            throw new NotPermittedException();

        if (!SsnLogic.IsValidSsn12(model.Ssn12))
            throw new InvalidSsnException();

        var ssn10 = model.Ssn12.ToSsn10();

        var ssn = await database.UserSsns
            .Where(x =>
                x.Ssn10 == ssn10 &&
                x.Ssn12 == model.Ssn12)
            .SingleOrDefaultAsync() ??
            throw new UserNotFoundException();

        var user = await database.Users
            .Where(x => x.Id == ssn.UserId)
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
            SignInBy = SignInBy.Ssn,
        };

        database.Sessions.Add(session);

        userToken.NoSave = false;

        var userEvent = new UserEvent()
        {
            UserId = user.Id,
            UserEventKind = UserEventKind.SignInBySsn,
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
        return _config.AllowSignInBySsn;
    }
}
