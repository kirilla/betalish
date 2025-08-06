namespace Betalish.Application.Routines.CreateInvoicePlan.Credit;

public class CreateCreditInvoicePlanRoutine(IDatabaseService database) 
    : ICreateCreditInvoicePlanRoutine
{
    public async Task Execute(
        IUserToken userToken, int invoiceId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        AssertIsCreditInvoice(invoice);

        CreateInvoicePlan(invoice);

        await database.SaveAsync(userToken);

        // NOTE: The dependent in a 1:1 relationship is added implicitly
        // via the Principal navigation property and must not be Add():ed
        // to the database context. Doing so will fail with a duplicate insertion error.
    }

    private void AssertIsCreditInvoice(Invoice invoice)
    {
        if (!invoice.IsCredit)
            throw new Exception(
                $"Invoice {invoice.Id} is not a credit invoice!");
    }

    private void CreateInvoicePlan(Invoice invoice)
    {
        // Distribution
        bool sendByEmail = invoice.Customer_Email.HasValue();
        bool sendPostal = !sendByEmail;

        var plan = new InvoicePlan()
        {
            SendByEmail = sendByEmail,
            SendPostal = sendPostal,
        };

        invoice.InvoicePlan = plan;
    }
}
