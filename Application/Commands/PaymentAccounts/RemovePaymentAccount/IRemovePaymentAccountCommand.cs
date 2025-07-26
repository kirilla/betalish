namespace Betalish.Application.Commands.PaymentAccounts.RemovePaymentAccount;

public interface IRemovePaymentAccountCommand
{
    Task Execute(IUserToken userToken, RemovePaymentAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
