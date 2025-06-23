namespace Betalish.Application.Commands.UserEmails.RemoveUserEmail;

public class RemoveUserEmailCommand(
    IDatabaseService database) : IRemoveUserEmailCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveUserEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var user = await database.Users
            .Where(x => x.Id == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var emails = await database.UserEmails
            .Where(x => x.UserId == userToken.UserId!.Value)
            .ToListAsync();

        if (emails.Count < 2)
            throw new NotPermittedException();

        var emailToRemove = emails
            .Where(x => x.Id == model.UserEmailId)
            .SingleOrDefault() ??
            throw new NotFoundException();

        if (emailToRemove.UserId != userToken.UserId!.Value)
            throw new NotPermittedException();

        database.UserEmails.Remove(emailToRemove);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
