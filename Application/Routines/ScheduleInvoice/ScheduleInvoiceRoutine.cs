namespace Betalish.Application.Routines.ScheduleInvoice;

public class ScheduleInvoiceRoutine(IDatabaseService database) : IScheduleInvoiceRoutine
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

        if (invoice.IsDebit)
        {
            var paymentTerms = await database.PaymentTerms
                .AsNoTracking()
                .Where(x =>
                    x.Id == paymentTermsId &&
                    x.ClientId == userToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ScheduleDebitInvoice(invoice, paymentTerms);
        }

        if (invoice.IsCredit)
        {
            ScheduleCreditInvoice(invoice);
        }

        await database.SaveAsync(userToken);
    }

    private static void ScheduleDebitInvoice(
        Invoice invoice, PaymentTerms paymentTerms)
    {
        invoice.DueDate = invoice.InvoiceDate
            .AddDays(paymentTerms.PaymentTermDays);

        if (paymentTerms.Reminder)
        {
            invoice.ReminderDate = invoice.DueDate!.Value
                .AddDays(Defaults.Invoice.Reminder.ReminderDays);

            invoice.ReminderDueDate = invoice.ReminderDate?
                .AddDays(Defaults.Invoice.Reminder.ReminderDueDays);
        }

        if (paymentTerms.Demand)
        {
            invoice.DemandDate = invoice.ReminderDueDate?
                .AddDays(Defaults.Invoice.Demand.DemandDays);

            invoice.DemandDueDate = invoice.DemandDate?
                .AddDays(Defaults.Invoice.Demand.DemandDueDays);
        }

        if (paymentTerms.Collect)
        {
            invoice.CollectDate = null;

            // TBD
        }
    }

    private static void ScheduleCreditInvoice(Invoice invoice)
    {
        invoice.DueDate = null;

        invoice.ReminderDate = null;
        invoice.ReminderDueDate = null;

        invoice.DemandDate = null;
        invoice.DemandDueDate = null;

        invoice.CollectDate = null;
    }
}
