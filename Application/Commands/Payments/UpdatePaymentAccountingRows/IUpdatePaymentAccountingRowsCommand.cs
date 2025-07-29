namespace Betalish.Application.Commands.Payments.UpdatePaymentAccountingRows;

public interface IUpdatePaymentAccountingRowsCommand
{
    Task Execute(IUserToken userToken, UpdatePaymentAccountingRowsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
