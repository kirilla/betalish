namespace Betalish.Application.Commands.Users.StripUserAdmin;

public class StripUserAdminCommand(IDatabaseService database) : IStripUserAdminCommand
{
    public async Task Execute(
        IUserToken userToken, StripUserAdminCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var user = await database.Users
            .Where(x => x.Id == model.UserId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var auth = await database.AdminAuths
            .Where(x => x.UserId == model.UserId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.AdminAuths.Remove(auth);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
