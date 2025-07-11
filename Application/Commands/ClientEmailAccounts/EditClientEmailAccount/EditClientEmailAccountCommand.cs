namespace Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

public class EditClientEmailAccountCommand(IDatabaseService database) : IEditClientEmailAccountCommand
{
    public async Task Execute(
        IUserToken userToken, 
        EditClientEmailAccountCommandModel model)
    {
        if (! await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var account = await database.ClientEmailAccounts
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        account.FromName = model.FromName;
        account.FromAddress = model.FromAddress;
        account.ReplyToName = model.ReplyToName;
        account.ReplyToAddress = model.ReplyToAddress;
        account.Password = model.Password;
        account.SmtpHost = model.SmtpHost;
        account.SmtpPort = model.SmtpPort;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
