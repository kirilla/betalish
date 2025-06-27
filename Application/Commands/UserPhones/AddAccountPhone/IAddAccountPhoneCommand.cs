namespace Betalish.Application.Commands.UserPhones.AddAccountPhone;

public interface IAddAccountPhoneCommand
{
    Task Execute(IUserToken userToken, AddAccountPhoneCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
