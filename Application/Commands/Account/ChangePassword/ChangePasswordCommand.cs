using Microsoft.AspNetCore.Identity;

namespace Betalish.Application.Commands.Account.ChangePassword;

public class ChangePasswordCommand(IDatabaseService database) : IChangePasswordCommand
{
    public async Task Execute(IUserToken userToken, ChangePasswordCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (model.NewPassword == model.OldPassword)
            throw new InvalidDataException();

        if (string.IsNullOrWhiteSpace(model.OldPassword) ||
            string.IsNullOrWhiteSpace(model.NewPassword))
            throw new InvalidDataException();

        var user = await database.Users
            .Where(x => x.Id == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var hasher = new PasswordHasher<User>();

        var result = hasher.VerifyHashedPassword(
            user, user.PasswordHash, model.OldPassword);

        if (result != PasswordVerificationResult.Success &&
            result != PasswordVerificationResult.SuccessRehashNeeded)
            throw new PasswordVerificationFailedException();

        var hash = hasher.HashPassword(user, model.NewPassword);

        user.PasswordHash = hash;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
