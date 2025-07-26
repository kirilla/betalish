namespace Betalish.Application.Commands.PaymentAccounts.EditPaymentAccount;

public interface IEditPaymentAccountCommand
{
    Task Execute(IUserToken userToken, EditPaymentAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
