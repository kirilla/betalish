namespace Betalish.Application.Commands.SessionRecords.RemoveSessionRecords;

public class RemoveSessionRecordsCommand(IDatabaseService database) : IRemoveSessionRecordsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveSessionRecordsCommandModel model)
    {
        if (!await IsPermitted(userToken))
            throw new NotPermittedException();

        var query = await database.SessionRecords.ExecuteDeleteAsync();
    }

    public async Task<bool> IsPermitted(IUserToken userToken)
    {
        return await database.AdminAuths.AnyAsync(x =>
            x.UserId == userToken.UserId!.Value);
    }
}
