namespace Betalish.Application.Commands.Payments.AssignPayment;

public interface IAssignPaymentCommand
{
    Task Execute(IUserToken userToken, AssignPaymentCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
