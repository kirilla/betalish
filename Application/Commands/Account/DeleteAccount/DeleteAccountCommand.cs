namespace Betalish.Application.Commands.Account.DeleteAccount;

public class DeleteAccountCommand(IDatabaseService database) : IDeleteAccountCommand
{
    public async Task Execute(IUserToken userToken, DeleteAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var user = await database.Users
            .Where(x => x.Id == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.Users.Remove(user);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
