using Betalish.Application.EmailTemplates.DemandEmail;

namespace Betalish.Application.Routines.SendDemandEmail;

public class SendDemandEmailRoutine(
    IDatabaseService database,
    ISmtpService smtpService,
    IDemandEmailTemplate demandTemplate)
    : ISendDemandEmailRoutine
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
                "Det går inte att skicka ett krav för en kreditfaktura.");

        if (string.IsNullOrWhiteSpace(invoice.Customer_Name))
            throw new MissingNameException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Email))
            throw new MissingEmailException();

        var plan = await database.InvoicePlans
            .AsNoTracking()
            .Where(x => x.Id == invoiceId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (invoice.DemandDate == null)
            throw new Exception(
                "Fakturan saknar DemandDate.");

        if (invoice.DemandDueDate == null)
            throw new Exception(
                "Fakturan saknar DemandDueDate.");

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == invoice.ClientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();
        
        var emailMessage = demandTemplate.Create(emailAccount, invoice, plan);

        smtpService.SendEmailMessage(emailAccount, emailMessage);
    }
}
