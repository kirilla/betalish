namespace Betalish.Application.Commands.Tests.TestLogItemDedup;

public interface ITestLogItemDedupCommand
{
    Task Execute(IUserToken userToken, TestLogItemDedupCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
