namespace Betalish.Application.Commands.Account.RegisterAccount;

public interface IRegisterAccountCommand
{
    Task Execute(IUserToken userToken, RegisterAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
