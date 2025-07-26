namespace Betalish.Application.Commands.PaymentAccounts.AddPaymentAccount;

public interface IAddPaymentAccountCommand
{
    Task<int> Execute(IUserToken userToken, AddPaymentAccountCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
