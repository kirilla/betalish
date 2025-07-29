using Betalish.Application.Queues.LogItems;
using Betalish.Application.Routines.ConvertDraftToInvoice;
using Betalish.Application.Routines.SetInvoiceNumber;
using Betalish.Application.Routines.UpdateInvoiceAccountingRows;

namespace Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

public class ApproveInvoiceDraftCommand(
    IDatabaseService database,
    ILogItemList logItemList,
    IConvertDraftToInvoiceRoutine convertToInvoiceRoutine,
    ISetInvoiceNumberRoutine setInvoiceNumberRoutine,
    IUpdateInvoiceAccountingRowsRoutine updateInvoiceAccounting) : IApproveInvoiceDraftCommand
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

        var draftBalanceRows = await database.DraftBalanceRows
            .AsNoTracking()
            .Where(x =>
                x.InvoiceDraftId == draft.Id &&
                x.InvoiceDraft.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        Assert(userToken, draft, draftRows, draftBalanceRows);

        int invoiceId = await convertToInvoiceRoutine.Execute(userToken, draft.Id);

        await setInvoiceNumberRoutine.Execute(userToken, invoiceId);

        await updateInvoiceAccounting.Execute(userToken, invoiceId);

        return invoiceId;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }

    private void Assert(
        IUserToken userToken, 
        InvoiceDraft draft,
        List<InvoiceDraftRow> draftRows,
        List<DraftBalanceRow> draftBalanceRows)
    {
        try
        {
            // Date
            AssertInvoiceDateGood(draft);

            // Total
            AssertTotalNotZero(draft);
            AssertTotalNegativeForCreditInvoice(draft);
            AssertTotalPositiveForDebitInvoice(draft);
            // AssertTotalAboveMinToConsiderPaid(draft);

            // Address
            AssertHasAddress(draft);

            // Rows
            AssertHasDraftRows(draftRows);
            AssertDraftRowsHaveVatAccount(draftRows);

            // Balance rows
            AssertCreditDraftHasBalanceRows(draft, draftBalanceRows);

            // Balance sum
            AssertBalanceSumTotalMatchesCreditDraftTotal(draft, draftBalanceRows);
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

    private static void AssertTotalNotZero(InvoiceDraft draft)
    {
        if (draft.Total == 0 && draft.TotalRounding == 0)
            throw new UserFeedbackException(
                "Fakturabeloppet får inte vara noll.");
    }

    private static void AssertTotalNegativeForCreditInvoice(InvoiceDraft draft)
    {
        if (draft.IsCredit && draft.Total > 0)
            throw new UserFeedbackException(
                "Belopp på kreditfaktura ska vara negativt.");
    }

    private static void AssertTotalPositiveForDebitInvoice(InvoiceDraft draft)
    {
        if (draft.IsCredit == false && draft.Total < 0)
            throw new UserFeedbackException(
                "Belopp på debetfaktura ska vara positivt.");
    }

    private static void AssertHasAddress(InvoiceDraft draft)
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

    private static void AssertHasDraftRows(List<InvoiceDraftRow> draftRows)
    {
        if (draftRows.Count == 0)
            throw new UserFeedbackException(
                "Utkastet saknar rader.");
    }

    private static void AssertDraftRowsHaveVatAccount(List<InvoiceDraftRow> draftRows)
    {
        if (draftRows.Any(x =>
            x.VatRate != 0 &&
            x.VatAccount.AccountIsValid() == false))
            throw new UserFeedbackException(
                "En rad i utkastet saknar moms-konto.");
    }

    private static void AssertCreditDraftHasBalanceRows(
        InvoiceDraft draft, 
        List<DraftBalanceRow> draftBalanceRows)
    {
        if (draft.IsCredit && draftBalanceRows.Count == 0)
        {
            throw new UserFeedbackException(
                "Utkastet saknar kvitteringsrader.");
        }
    }

    private static void AssertBalanceSumTotalMatchesCreditDraftTotal(
        InvoiceDraft draft,
        List<DraftBalanceRow> draftBalanceRows)
    {
        if (draft.IsCredit &&
            draftBalanceRows.Sum(x => x.Amount) != -draft.Total)
        {
            throw new UserFeedbackException(
                "Kvitteringen överensstämmer inte med fakturabeloppet.");
        }
    }

    private static void AssertInvoiceDateGood(InvoiceDraft draft)
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
