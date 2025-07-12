namespace Betalish.Application.Commands.Sessions.RemoveSessionRecords;

public interface IRemoveSessionRecordsCommand
{
    Task Execute(IUserToken userToken, RemoveSessionRecordsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
