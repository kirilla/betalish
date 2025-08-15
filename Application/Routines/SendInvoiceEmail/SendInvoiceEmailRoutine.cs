using Betalish.Application.EmailTemplates.CreditInvoiceEmail;
using Betalish.Application.EmailTemplates.DebitInvoiceEmail;

namespace Betalish.Application.Routines.SendInvoiceEmail;

public class SendInvoiceEmailRoutine(
    IDatabaseService database,
    ISmtpService smtpService,
    IDebitInvoiceEmailTemplate debitTemplate,
    ICreditInvoiceEmailTemplate creditTemplate)
    : ISendInvoiceEmailRoutine
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

        if (string.IsNullOrWhiteSpace(invoice.Customer_Name))
            throw new MissingNameException();

        if (string.IsNullOrWhiteSpace(invoice.Customer_Email))
            throw new MissingEmailException();

        var emailAccount = await database.EmailAccounts
            .AsNoTracking()
            .Where(x => x.ClientId == invoice.ClientId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        EmailMessage emailMessage = null!;

        if (invoice.IsDebit)
        {
            emailMessage = debitTemplate.Create(emailAccount, invoice);
        }

        if (invoice.IsCredit)
        {
            emailMessage = creditTemplate.Create(emailAccount, invoice);
        }

        smtpService.SendEmailMessage(emailAccount, emailMessage);
    }
}
