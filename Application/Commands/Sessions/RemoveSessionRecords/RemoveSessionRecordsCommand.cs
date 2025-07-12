namespace Betalish.Application.Commands.Sessions.RemoveSessionRecords;

public class RemoveSessionRecordsCommand(IDatabaseService database) : IRemoveSessionRecordsCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveSessionRecordsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        await database.SessionRecords.ExecuteDeleteAsync();
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAdmin;
    }
}
