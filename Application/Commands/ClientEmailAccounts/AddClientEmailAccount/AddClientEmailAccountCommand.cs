namespace Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

public class AddClientEmailAccountCommand(IDatabaseService database) : IAddClientEmailAccountCommand
{
    public async Task<int> Execute(
        IUserToken userToken, 
        AddClientEmailAccountCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.ClientEmailAccounts
            .AnyAsync(x => x.ClientId == userToken.ClientId!.Value))
            throw new BlockedByExistingException();

        var account = new ClientEmailAccount()
        {
            FromName = model.FromName,
            FromAddress = model.FromAddress,
            ReplyToName = model.ReplyToName,
            ReplyToAddress = model.ReplyToAddress,
            Password = model.Password,
            SmtpHost = model.SmtpHost,
            SmtpPort = model.SmtpPort,
            ClientId = userToken.ClientId!.Value,
        };

        database.ClientEmailAccounts.Add(account);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
