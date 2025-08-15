using Betalish.Application.EmailTemplates.ReminderEmail;

namespace Betalish.Application.Routines.SendReminderEmail;

public class SendReminderEmailRoutine(
    IDatabaseService database,
    ISmtpService smtpService,
    IReminderEmailTemplate reminderTemplate)
    : ISendReminderEmailRoutine
{
    public async Task Execute(IUserToken userToken, int invoiceId)
    {
        database.ChangeTracker.Clear();

        var invoice = await database.Invoices
            .AsNoTracking()
            .Where(x =>
                x.Id == invoiceId &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (invoice.IsCredit)
            throw new Exception(
                "Det går inte att skicka en påminnelse för en kreditfaktura.");

        if (string.IsNullOrWhiteSpace(invoice.Customer_Name))
            throw new MissingNameException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Email))
            throw new MissingEmailException();

        var plan = await database.InvoicePlans
            .AsNoTracking()
            .Where(x => x.Id == invoiceId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (invoice.ReminderDate == null)
            throw new Exception(
                "Fakturan saknar ReminderDate.");

        if (invoice.ReminderDueDate == null)
            throw new Exception(
                "Fakturan saknar ReminderDueDate.");

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == invoice.ClientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();
        
        var emailMessage = reminderTemplate.Create(emailAccount, invoice, plan);

        smtpService.SendEmailMessage(emailAccount, emailMessage);
    }
}
