namespace Betalish.Application.Commands.ClientEmailAccounts.SetClientEmailAccountPassword;

public interface ISetClientEmailAccountPasswordCommand
{
    Task Execute(IUserToken userToken, SetClientEmailAccountPasswordCommandModel model);

    Task<bool> IsPermitted(IUserToken userToken);
}
