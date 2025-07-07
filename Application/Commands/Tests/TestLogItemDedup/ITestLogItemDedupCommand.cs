namespace Betalish.Application.Commands.Tests.TestLogItemDedup;

public interface ITestLogItemDedupCommand
{
    Task Execute(IUserToken userToken, TestLogItemDedupCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
