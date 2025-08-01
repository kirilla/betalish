using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IDatabaseService
{
    DbSet<AdminAuth> AdminAuths { get; set; }
    DbSet<Article> Articles { get; set; }
    DbSet<BadSignIn> BadSignIns { get; set; }
    DbSet<BalanceRow> BalanceRows { get; set; }
    DbSet<Batch> Batches { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<ClientAuth> ClientAuths { get; set; }
    DbSet<ClientEmailAccount> ClientEmailAccounts { get; set; }
    DbSet<ClientEmailMessage> ClientEmailMessages { get; set; }
    DbSet<ClientEvent> ClientEvents { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<CustomerTag> CustomerTags { get; set; }
    DbSet<DraftBalanceRow> DraftBalanceRows { get; set; }
    DbSet<EmailAttachment> EmailAttachments { get; set; }
    DbSet<EmailImage> EmailImages { get; set; }
    DbSet<EmailMessage> EmailMessages { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<InvoiceAccountingRow> InvoiceAccountingRows { get; set; }
    DbSet<InvoiceFee> InvoiceFees { get; set; }
    DbSet<InvoiceRow> InvoiceRows { get; set; }
    DbSet<InvoiceDraft> InvoiceDrafts { get; set; }
    DbSet<InvoiceDraftRow> InvoiceDraftRows { get; set; }
    DbSet<InvoiceRange> InvoiceRanges { get; set; }
    DbSet<InvoiceTemplate> InvoiceTemplates { get; set; }
    DbSet<InvoiceTemplateRow> InvoiceTemplateRows { get; set; }
    DbSet<LedgerAccount> LedgerAccounts { get; set; }
    DbSet<LogItem> LogItems { get; set; }
    DbSet<NetworkRequest> NetworkRequests { get; set; }
    DbSet<NetworkRule> NetworkRules { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<PaymentAccount> PaymentAccounts { get; set; }
    DbSet<PaymentAccountingRow> PaymentAccountingRows { get; set; }
    DbSet<Session> Sessions { get; set; }
    DbSet<SessionActivity> SessionActivities { get; set; }
    DbSet<SessionRecord> SessionRecords { get; set; }
    DbSet<Signup> Signups { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserEmail> UserEmails { get; set; }
    DbSet<UserEvent> UserEvents { get; set; }
    DbSet<UserPhone> UserPhones { get; set; }
    DbSet<UserSsn> UserSsns { get; set; }

    Task SaveAsync(
        IUserToken userToken,
        CancellationToken cancellation = default);

    ChangeTracker ChangeTracker { get; }
}
