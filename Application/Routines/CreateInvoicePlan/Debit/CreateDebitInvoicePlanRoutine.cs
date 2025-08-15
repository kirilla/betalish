namespace Betalish.Application.Routines.CreateInvoicePlan.Debit;

public class CreateDebitInvoicePlanRoutine(IDatabaseService database) 
    : ICreateDebitInvoicePlanRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId, int? paymentTermsId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        AssertIsDebitInvoice(invoice);

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

        AssertHasPaymentTerms(invoice, paymentTerms);

        CreateInvoicePlan(invoice, paymentTerms!);

        await database.SaveAsync(userToken);

        // NOTE: The dependent in a 1:1 relationship is added implicitly
        // via the Principal navigation property.
    }

    private void AssertIsDebitInvoice(Invoice invoice)
    {
        if (!invoice.IsDebit)
            throw new Exception(
                $"Invoice {invoice.Id} is not a debit invoice!");
    }

    private void AssertHasPaymentTerms(Invoice invoice, PaymentTerms? paymentTerms)
    {
        if (paymentTerms == null)
            throw new Exception(
                $"Missing PaymentTerms for debit Invoice {invoice.Id}.");
    }

    private void CreateInvoicePlan(Invoice invoice, PaymentTerms paymentTerms)
    {
        var plan = new InvoicePlan()
        {
        };

        invoice.InvoicePlan = plan;
    }
}
