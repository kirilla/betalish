namespace Betalish.Application.Commands.Payments.RemovePayment;

public interface IRemovePaymentCommand
{
    Task Execute(IUserToken userToken, RemovePaymentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
