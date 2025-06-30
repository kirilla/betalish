namespace Betalish.Application.Commands.SessionRecords.RemoveSessionRecords;

public interface IRemoveSessionRecordsCommand
{
    Task Execute(IUserToken userToken, RemoveSessionRecordsCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
