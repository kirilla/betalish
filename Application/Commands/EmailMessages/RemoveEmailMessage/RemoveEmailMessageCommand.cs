namespace Betalish.Application.Commands.EmailMessages.RemoveEmailMessage;

public class RemoveEmailMessageCommand(IDatabaseService database) : IRemoveEmailMessageCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveEmailMessageCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var message = await database.EmailMessages
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.EmailMessages.Remove(message);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
