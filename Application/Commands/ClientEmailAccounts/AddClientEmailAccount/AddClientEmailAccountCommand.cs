namespace Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

public class AddClientEmailAccountCommand(IDatabaseService database) : IAddClientEmailAccountCommand
{
    public async Task<int> Execute(
        IUserToken userToken, 
        AddClientEmailAccountCommandModel model,
        int clientId)
    {
        if (!await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var client = await database.Clients
            .Where(x => x.Id == model.ClientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.ClientEmailAccounts
            .AnyAsync(x => 
                x.ClientId == model.ClientId &&
                x.FromAddress == model.FromAddress))
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
            ClientId = model.ClientId,
        };

        database.ClientEmailAccounts.Add(account);

        await database.SaveAsync(userToken);

        return account.Id;
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
