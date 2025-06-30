namespace Betalish.Application.Commands.UserEmails.AddAccountEmail;

public class AddAccountEmailCommand(
    IDatabaseService database) : IAddAccountEmailCommand
{
    public async Task Execute(
        IUserToken userToken, AddAccountEmailCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Address = model.Address.Trim().ToLowerInvariant();

        if (await database.UserEmails
            .Where(x => x.UserId == userToken.UserId!.Value)
            .CountAsync() >= Limits.User.EmailAddresses.Max)
            throw new TooManyException();

        if (await database.UserEmails
            .AnyAsync(x =>
                x.UserId == userToken.UserId!.Value &&
                x.Address == model.Address))
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
