using Betalish.Application.Queues.LogItems;
using Betalish.Application.Routines.CreateInvoicePlan.Credit;
using Betalish.Application.Routines.CreateInvoicePlan.Debit;

namespace Betalish.Application.Routines.CreateInvoicePlan;

public class CreateInvoicePlanRoutine(
    IDatabaseService database,
    ILogItemList logItemList,
    ICreateDebitInvoicePlanRoutine createDebitInvoicePlan,
    ICreateCreditInvoicePlanRoutine createCreditInvoicePlan) : ICreateInvoicePlanRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId, int? paymentTermsId)
    {
        try
        {
            database.ChangeTracker.Clear();

            var invoice = await database.Invoices
                .AsNoTracking()
                .Where(x =>
                    x.Id == invoiceId &&
                    x.ClientId == userToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            AssertInvoiceStatusIssued(invoice);
            AssertHasInvoiceNumber(invoice);

            if (invoice.IsDebit)
            {
                await createDebitInvoicePlan.Execute(userToken, invoiceId, paymentTermsId);
            }

            if (invoice.IsCredit)
            {
                await createCreditInvoicePlan.Execute(userToken, invoiceId);
            }
        }
        catch (Exception ex)
        {
            logItemList.AddLogItem(new LogItem(ex)
            {
                Source = nameof(CreateInvoicePlanRoutine),
            });

            throw new UserFeedbackException(
                "Fel i intern rutin för skapande av fakturaplanering. " +
                "Kontakta administratören.");
        }
    }

    private void AssertInvoiceStatusIssued(Invoice invoice)
    {
        if (invoice.InvoiceStatus != InvoiceStatus.Issued)
            throw new Exception(
                $"Invoice {invoice.Id}, " +
                $"Expected InvoiceStatus: Issued, " +
                $"Actual InvoiceStatus: {invoice.InvoiceStatus}.");
    }

    private void AssertHasInvoiceNumber(Invoice invoice)
    {
        if (invoice.InvoiceNumber == null)
            throw new Exception(
                $"Invoice {invoice.Id}, " +
                $"Expected InvoiceNumber: non-null, " +
                $"Actual InvoiceNumber: null.");
    }
}
