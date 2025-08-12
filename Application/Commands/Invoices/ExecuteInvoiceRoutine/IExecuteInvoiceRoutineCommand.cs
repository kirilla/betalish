namespace Betalish.Application.Commands.Invoices.ExecuteInvoiceRoutine;

public interface IExecuteInvoiceRoutineCommand
{
    Task Execute(IUserToken userToken, ExecuteInvoiceRoutineCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
