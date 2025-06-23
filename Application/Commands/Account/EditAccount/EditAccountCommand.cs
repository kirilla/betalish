namespace Betalish.Application.Commands.Account.EditAccount;

public class EditAccountCommand(
    IDatabaseService database) : IEditAccountCommand
{
    public async Task Execute(IUserToken userToken, EditAccountCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (string.IsNullOrWhiteSpace(model.Name))
            throw new NotPermittedException();

        var user = await database.Users
            .Where(x => x.Id == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        user.Name = model.Name;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
