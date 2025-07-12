namespace Betalish.Application.Commands.ClientEmailAccounts.SetClientEmailAccountPassword;

public interface ISetClientEmailAccountPasswordCommand
{
    Task Execute(IUserToken userToken, SetClientEmailAccountPasswordCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
