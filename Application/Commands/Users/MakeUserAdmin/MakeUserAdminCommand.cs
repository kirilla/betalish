namespace Betalish.Application.Commands.Users.MakeUserAdmin;

public class MakeUserAdminCommand(IDatabaseService database) : IMakeUserAdminCommand
{
    public async Task Execute(
        IUserToken userToken, MakeUserAdminCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var user = await database.Users
            .Where(x => x.Id == model.UserId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.AdminAuths
            .AnyAsync(x => x.UserId == userToken.UserId!.Value))
            throw new BlockedByExistingException();

        var auth = new AdminAuth()
        {
            UserId = user.Id,
        };

        database.AdminAuths.Add(auth);

        user.UserPurpose = UserPurpose.Admin;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
