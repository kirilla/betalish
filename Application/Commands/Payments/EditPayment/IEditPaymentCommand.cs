namespace Betalish.Application.Commands.Payments.EditPayment;

public interface IEditPaymentCommand
{
    Task Execute(IUserToken userToken, EditPaymentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
