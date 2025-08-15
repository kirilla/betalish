using Betalish.Application.EmailTemplates.CollectEmail;

namespace Betalish.Application.Routines.SendCollectEmail;

public class SendCollectEmailRoutine(
    IDatabaseService database,
    ISmtpService smtpService,
    ICollectEmailTemplate demandTemplate)
    : ISendCollectEmailRoutine
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
                "Det går inte att skicka en KFM-varning om en kreditfaktura.");

        if (string.IsNullOrWhiteSpace(invoice.Customer_Name))
            throw new MissingNameException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Email))
            throw new MissingEmailException();

        var plan = await database.InvoicePlans
            .AsNoTracking()
            .Where(x => x.Id == invoiceId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (plan.CollectDate == null)
            throw new Exception(
                "InvoicePlan för fakturan saknar CollectDate.");

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == invoice.ClientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();
        
        var emailMessage = demandTemplate.Create(emailAccount, invoice, plan);

        smtpService.SendEmailMessage(emailAccount, emailMessage);
    }
}
