namespace Betalish.Application.Commands.UserEmails.RemoveAccountEmail;

public class RemoveAccountEmailCommand(
    IDatabaseService database) : IRemoveAccountEmailCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveAccountEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        if (await database.UserEmails
            .Where(x => x.UserId == userToken.UserId!.Value)
            .CountAsync() < 2)
            throw new NotPermittedException();

        var email = await database.UserEmails
            .Where(x => 
                x.Id == model.Id &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.UserEmails.Remove(email);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
