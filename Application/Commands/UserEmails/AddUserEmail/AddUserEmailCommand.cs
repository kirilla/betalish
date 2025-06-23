namespace Betalish.Application.Commands.UserEmails.AddUserEmail;

public class AddUserEmailCommand(
    IDatabaseService database) : IAddUserEmailCommand
{
    public async Task Execute(
        IUserToken userToken, AddUserEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Address = model.Address.Trim().ToLowerInvariant();

        var user = database.Users
            .Where(x => x.Id == userToken.UserId!.Value)
            .SingleOrDefault() ??
            throw new NotFoundException();

        var emails = await database.UserEmails
            .Where(x => x.UserId == userToken.UserId!.Value)
            .ToListAsync();

        if (emails.Any(x => x.Address == model.Address))
            throw new BlockedByExistingException();

        var email = new UserEmail()
        {
            UserId = userToken.UserId!.Value,
            Address = model.Address,
        };

        database.UserEmails.Add(email);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
