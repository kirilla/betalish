namespace Betalish.Application.Commands.Payments.UnassignPayment;

public interface IUnassignPaymentCommand
{
    Task Execute(IUserToken userToken, UnassignPaymentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
