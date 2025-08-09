namespace Betalish.Application.Commands.EmailAccounts.SetEmailAccountPassword;

public class SetEmailAccountPasswordCommand(IDatabaseService database) : ISetEmailAccountPasswordCommand
{
    public async Task Execute(
        IUserToken userToken, 
        SetEmailAccountPasswordCommandModel model)
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

        account.Password = model.Password;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
