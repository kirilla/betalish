namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public class EditClientEmailAccountCommand(IDatabaseService database) : IEditClientEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        EditClientEmailAccountCommandModel model)
    {
        if (! IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.EmailAccounts
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        account.FromName = model.FromName;
        account.FromAddress = model.FromAddress;
        account.ReplyToName = model.ReplyToName;
        account.ReplyToAddress = model.ReplyToAddress;
        account.SmtpHost = model.SmtpHost;
        account.SmtpPort = model.SmtpPort;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
