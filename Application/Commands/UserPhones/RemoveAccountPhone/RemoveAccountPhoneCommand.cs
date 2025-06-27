namespace Betalish.Application.Commands.UserPhones.RemoveAccountPhone;

public class RemoveAccountPhoneCommand(IDatabaseService database) : IRemoveAccountPhoneCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveAccountPhoneCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        var phone = await database.UserPhones
            .Where(x =>
                x.Id == model.Id!.Value &&
                x.UserId == userToken.UserId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.UserPhones.Remove(phone);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
