using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IDatabaseService
{
    DbSet<AdminAuth> AdminAuths { get; set; }
    DbSet<BlockedRequest> BlockedRequests { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<ClientAuth> ClientAuths { get; set; }
    DbSet<ClientEmailAccount> ClientEmailAccounts { get; set; }
    DbSet<ClientEmailMessage> ClientEmailMessages { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<EmailAttachment> EmailAttachments { get; set; }
    DbSet<EmailImage> EmailImages { get; set; }
    DbSet<EmailMessage> EmailMessages { get; set; }
    DbSet<NetworkRule> NetworkRules { get; set; }
    DbSet<Session> Sessions { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserEmail> UserEmails { get; set; }

    Task SaveAsync(IUserToken userToken);

    ChangeTracker ChangeTracker { get; }
}
