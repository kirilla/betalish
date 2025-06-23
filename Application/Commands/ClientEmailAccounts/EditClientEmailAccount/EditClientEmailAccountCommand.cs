namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public class EditClientEmailAccountCommand(IDatabaseService database) : IEditClientEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        EditClientEmailAccountCommandModel model, 
        int clientId)
    {
        if (! await IsPermitted(userToken, clientId))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.ClientEmailAccounts
            .Where(x => x.Id == model.ClientEmailAccountId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.ClientEmailAccounts
            .AnyAsync(x =>
                x.FromName == model.FromName &&
                x.Id != model.ClientEmailAccountId))
            throw new BlockedByNameException();

        account.FromName = model.FromName;
        account.FromAddress = model.FromAddress;
        account.ReplyToName = model.ReplyToName;
        account.ReplyToAddress = model.ReplyToAddress;
        account.Password = model.Password;
        account.SmtpHost = model.SmtpHost;
        account.SmtpPort = model.SmtpPort;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken, int clientId)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == clientId &&
            x.UserId == userToken.UserId!.Value);
    }
}
