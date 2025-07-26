namespace Betalish.Application.Commands.Payments.AddPayment;

public interface IAddPaymentCommand
{
    Task<int> Execute(IUserToken userToken, AddPaymentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
