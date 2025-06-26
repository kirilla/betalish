using Betalish.Common.Validation;

namespace Betalish.Application.Commands.Users.EditUser;

public class EditUserCommand(IDatabaseService database) : IEditUserCommand
{
    public async Task Execute(IUserToken userToken, EditUserCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (string.IsNullOrWhiteSpace(model.Name))
            throw new NotPermittedException();

        var user = await database.Users
            .Where(x => x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        user.Name = model.Name;
        user.NoLogin = model.NoLogin;
        user.NoSave = model.NoSave;

        await database.SaveAsync(userToken);
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
