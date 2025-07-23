using Betalish.Application.Queues.LogItems;
using Betalish.Application.Routines.ConvertDraftToInvoice;
using Betalish.Application.Routines.SetInvoiceNumber;

namespace Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

public class ApproveInvoiceDraftCommand(
    IDatabaseService database,
    ILogItemList logItemList,
    IConvertDraftToInvoiceRoutine convertToInvoiceRoutine,
    ISetInvoiceNumberRoutine setInvoiceNumberRoutine) : IApproveInvoiceDraftCommand
{
    public async Task<int> Execute(
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

        var draftRows = await database.InvoiceDraftRows
            .AsNoTracking()
            .Where(x =>
                x.InvoiceDraftId == draft.Id &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        Assert(userToken, draft, draftRows);

        int invoiceId = await convertToInvoiceRoutine.Execute(userToken, draft.Id);

        await setInvoiceNumberRoutine.Execute(userToken, invoiceId);

        return invoiceId;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }

    private void Assert(
        IUserToken userToken, 
        InvoiceDraft draft, 
        List<InvoiceDraftRow> draftRows)
    {
        try
        {
            // Date
            AssertInvoiceDateGood(draft);

            // Total
            AssertTotalNotZero(draft, draftRows);
            AssertTotalNegativeForCreditInvoice(draft, draftRows);
            AssertTotalPositiveForDebitInvoice(draft, draftRows);
            // AssertTotalAboveMinToConsiderPaid(draft);

            // Address
            AssertHasAddress(draft);

            // Rows
            AssertHasDraftRows(draft, draftRows);

            // Balance rows
            // AssertCreditDraftHasBalanceRows();

            // Balance sum
            // AssertBalanceSumTotalMatchesCreditDraftTotal();
        }
        catch (Exception ex)
        {
            string routine = nameof(ConvertDraftToInvoiceRoutine);

            logItemList.AddLogItem(new LogItem(ex)
            {
                Description = $"{routine}: {ex.Message}",
                LogItemKind = LogItemKind.Assertion,
                UserId = userToken.UserId,
            });

            throw;
        }
    }

    private void AssertTotalNotZero(InvoiceDraft draft, List<InvoiceDraftRow> draftRows)
    {
        if (draft.Total == 0 && draft.TotalRounding == 0)
            throw new UserFeedbackException(
                "Fakturabeloppet får inte vara noll.");
    }

    private void AssertTotalNegativeForCreditInvoice(InvoiceDraft draft, List<InvoiceDraftRow> draftRows)
    {
        if (draft.IsCredit && draft.Total > 0)
            throw new UserFeedbackException(
                "Belopp på kreditfaktura ska vara negativt.");
    }

    private void AssertTotalPositiveForDebitInvoice(InvoiceDraft draft, List<InvoiceDraftRow> draftRows)
    {
        if (draft.IsCredit == false && draft.Total < 0)
            throw new UserFeedbackException(
                "Belopp på debetfaktura ska vara positivt.");
    }

    private void AssertHasAddress(InvoiceDraft draft)
    {
        if (draft.Customer_ZipCode.IsMissingValue() ||
            draft.Customer_City.IsMissingValue())
        {
            // Postnr + postort är minsta möjliga adress,
            // i en svensk kontext, men kanske vanligast för 
            // myndigheter.

            throw new UserFeedbackException(
                "Utkastet saknar address.");
        }
    }

    private void AssertHasDraftRows(InvoiceDraft draft, List<InvoiceDraftRow> draftRows)
    {
        if (draftRows.Count == 0)
            throw new UserFeedbackException(
                "Utkastet saknar rader.");
    }

    /*
    private void AssertCreditDraftHasBalanceRows(
        InvoiceDraft draft, 
        List<InvoiceDraftRow> draftRows,
        List<InvoiceDraftBalanceRow> balanceRows)
    {
        if (draft.IsCredit && balanceRows.Count == 0)
        {
            throw new UserFeedbackException(
                "Utkastet saknar kvitteringsrader.");
        }
    }
    */

    /*
    private void AssertBalanceSumTotalMatchesCreditDraftTotal(
        InvoiceDraft draft,
        List<InvoiceDraftRow> draftRows,
        List<InvoiceDraftBalanceRow> balanceRows)
    {
        if (draft.IsCredit &&
            balanceRows.Sum(x => x.Amount) != -draft.Total)
        {
            throw new UserFeedbackException(
                "Kvitteringen överensstämmer inte med fakturabeloppet.");
        }
    }
    */

    private void AssertInvoiceDateGood(InvoiceDraft draft)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (draft.InvoiceDate.HasValue &&
            draft.InvoiceDate.Value < today)
        {
            throw new UserFeedbackException(
                "Fakturadatum har passerat.");
        }
    }

    /*
    private void AssertTotalAboveMinToConsiderPaid(
        Client client,
        InvoiceDraft draft,
        List<InvoiceDraftRow> draftRows)
    {
        if (draft.IsCredit == false &&
            draft.Total < client.MinToConsiderPaid)
        {
            throw new UserFeedbackException(
                "Fakturabeloppet är under beloppsgränsen.");
        }
    }
    */
}
