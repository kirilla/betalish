namespace Betalish.Application.Commands.Tests.TestLogItemDedup;

public interface ITestLogItemDedupCommand
{
    void Execute(IUserToken userToken, TestLogItemDedupCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
