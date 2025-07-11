namespace Betalish.Application.Commands.LogItems.RemoveLogItems;

public interface IRemoveLogItemsCommand
{
    Task Execute(IUserToken userToken, RemoveLogItemsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
