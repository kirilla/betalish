namespace Betalish.Application.Commands.Signups.RemoveSignup;

public class RemoveSignupCommand(IDatabaseService database) : IRemoveSignupCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveSignupCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var signup = await database.Signups
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Signups.Remove(signup);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
