namespace Betalish.Application.Commands.ClientEmailAccounts.SetClientEmailAccountPassword;

public class SetClientEmailAccountPasswordCommand(IDatabaseService database) : ISetClientEmailAccountPasswordCommand
{
    public async Task Execute(
        IUserToken userToken, 
        SetClientEmailAccountPasswordCommandModel model)
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

        account.Password = model.Password;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.ClientAuths.AnyAsync(x =>
            x.ClientId == userToken.ClientId!.Value &&
            x.UserId == userToken.UserId!.Value);
    }
}
