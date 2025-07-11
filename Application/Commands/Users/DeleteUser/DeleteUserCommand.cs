namespace Betalish.Application.Commands.Users.DeleteUser;

public class DeleteUserCommand(
    IDatabaseService database) : IDeleteUserCommand
{
    public async Task Execute(IUserToken userToken, DeleteUserCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (model.Id == userToken.UserId)
            throw new Exception("Self-deletion not permitted.");

        var user = await database.Users
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Users.Remove(user);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
