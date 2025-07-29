using Betalish.Application.Routines.UpdatePaymentAccountingRows;

namespace Betalish.Application.Commands.Payments.UpdatePaymentAccountingRows;

public class UpdatePaymentAccountingRowsCommand(
    IDatabaseService database,
    IUpdatePaymentAccountingRowsRoutine updateAccountingRows) : IUpdatePaymentAccountingRowsCommand
{
    public async Task Execute(
        IUserToken userToken, UpdatePaymentAccountingRowsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var payment = await database.Payments
            .AsNoTracking()
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        await updateAccountingRows.Execute(userToken, payment.Id);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
