namespace Betalish.Application.Routines.UpdatePaymentAccountingRows;

public interface IUpdatePaymentAccountingRowsRoutine
{
    Task Execute(IUserToken userToken, int paymentId);
}
