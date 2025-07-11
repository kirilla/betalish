namespace Betalish.Application.Commands.SessionRecords.RemoveSessionRecords;

public interface IRemoveSessionRecordsCommand
{
    Task Execute(IUserToken userToken, RemoveSessionRecordsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
