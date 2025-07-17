using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IDatabaseService
{
    DbSet<AdminAuth> AdminAuths { get; set; }
    DbSet<Article> Articles { get; set; }
    DbSet<BadSignIn> BadSignIns { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<ClientAuth> ClientAuths { get; set; }
    DbSet<ClientEmailAccount> ClientEmailAccounts { get; set; }
    DbSet<ClientEmailMessage> ClientEmailMessages { get; set; }
    DbSet<ClientEvent> ClientEvents { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<EmailAttachment> EmailAttachments { get; set; }
    DbSet<EmailImage> EmailImages { get; set; }
    DbSet<EmailMessage> EmailMessages { get; set; }
    DbSet<LedgerAccount> LedgerAccounts { get; set; }
    DbSet<LogItem> LogItems { get; set; }
    DbSet<NetworkRequest> NetworkRequests { get; set; }
    DbSet<NetworkRule> NetworkRules { get; set; }
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
