using Betalish.Application.Routines.ConvertDraftToInvoiceRoutine;
using Betalish.Application.Routines.SetInvoiceNumber;

namespace Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

public class ApproveInvoiceDraftCommand(
    IDatabaseService database,
    IConvertDraftToInvoiceRoutine convertToInvoiceRoutine,
    ISetInvoiceNumberRoutine setInvoiceNumberRoutine) : IApproveInvoiceDraftCommand
{
    public async Task Execute(
        IUserToken userToken, ApproveInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        // TODO: Assert routines

        int invoiceId = await convertToInvoiceRoutine.Execute(userToken, draft.Id);

        await setInvoiceNumberRoutine.Execute(userToken, invoiceId);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
