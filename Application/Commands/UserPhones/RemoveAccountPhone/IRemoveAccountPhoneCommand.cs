namespace Betalish.Application.Commands.UserPhones.RemoveAccountPhone;

public interface IRemoveAccountPhoneCommand
{
    Task Execute(IUserToken userToken, RemoveAccountPhoneCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
