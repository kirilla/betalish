using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Routines.CreateInvoicePlan;

public class CreateInvoicePlanRoutine(
    IDatabaseService database,
    ILogItemList logItemList) : ICreateInvoicePlanRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId, int? paymentTermsId)
    {
        try
        {
            database.ChangeTracker.Clear();

            var invoice = await database.Invoices
                .Where(x =>
                    x.Id == invoiceId &&
                    x.ClientId == userToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            AssertInvoiceStatusIssued(invoice);
            AssertHasInvoiceNumber(invoice);

            PaymentTerms? paymentTerms = null;

            if (paymentTermsId.HasValue)
            {
                paymentTerms = await database.PaymentTerms
                    .AsNoTracking()
                    .Where(x =>
                        x.Id == paymentTermsId &&
                        x.ClientId == userToken.ClientId!.Value)
                    .SingleOrDefaultAsync();
            }

            AssertDebitInvoiceHasPaymentTerms(invoice, paymentTerms);
            AssertCreditInvoiceDoesNotHavePaymentTerms(invoice, paymentTerms);

            //database.Invoices.Attach(invoice);

            if (invoice.IsDebit)
                CreateDebitInvoicePlan(invoice, paymentTerms);

            if (invoice.IsCredit)
                CreateCreditInvoicePlan(invoice, paymentTerms);

            await database.SaveAsync(userToken);
        }
        catch (Exception ex)
        {
            logItemList.AddLogItem(new LogItem(ex)
            {
                Source = nameof(CreateInvoicePlanRoutine),
            });

            throw new UserFeedbackException(
                "Fel i intern rutin för skapande av fakturaplanering. Kontakta administratören.");
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

    private void AssertDebitInvoiceHasPaymentTerms(Invoice invoice, PaymentTerms? paymentTerms)
    {
        if (invoice.IsDebit && paymentTerms == null)
            throw new Exception(
                $"Missing PaymentTerms for debit Invoice {invoice.Id}.");
    }

    private void AssertCreditInvoiceDoesNotHavePaymentTerms(Invoice invoice, PaymentTerms? paymentTerms)
    {
        if (invoice.IsCredit && paymentTerms != null)
            throw new Exception(
                $"Unexpected PaymentTerms present for credit Invoice {invoice.Id}.");
    }

    private void CreateDebitInvoicePlan(Invoice invoice, PaymentTerms? paymentTerms)
    {
        var invoicePlan = new InvoicePlan()
        {
            Invoice = invoice,

            // TODO
        };

        // NOTE: The dependent in a 1:1 relationship is added implicitly
        // via the Principal navigation property and must not be Add():ed
        // to the database context. Doing so will fail with a duplicate insertion error.
    }

    private void CreateCreditInvoicePlan(Invoice invoice, PaymentTerms? paymentTerms)
    {
        var invoicePlan = new InvoicePlan()
        {
            Invoice = invoice,

            // TODO
        };

        // NOTE: The dependent in a 1:1 relationship is added implicitly
        // via the Principal navigation property and must not be Add():ed
        // to the database context. Doing so will fail with a duplicate insertion error.
    }
}
