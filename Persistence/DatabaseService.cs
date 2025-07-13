using Betalish.Persistence.Configuration;

namespace Betalish.Persistence;

public class DatabaseService(
    DbContextOptions<DatabaseService> options,
    IOnSaveFormatter formatter,
    IOnSaveValidator validator,
    ICreatedDateTimeSetter createdDateTimeSetter,
    IUpdatedDateTimeSetter updatedDateTimeSetter) : DbContext(options), IDatabaseService
{
    public DbSet<AdminAuth> AdminAuths { get; set; }
    public DbSet<BadSignIn> BadSignIns { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientAuth> ClientAuths { get; set; }
    public DbSet<ClientEmailAccount> ClientEmailAccounts { get; set; }
    public DbSet<ClientEmailMessage> ClientEmailMessages { get; set; }
    public DbSet<ClientEvent> ClientEvents { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EmailAttachment> EmailAttachments { get; set; }
    public DbSet<EmailImage> EmailImages { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<LogItem> LogItems { get; set; }
    public DbSet<NetworkRequest> NetworkRequests { get; set; }
    public DbSet<NetworkRule> NetworkRules { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionActivity> SessionActivities { get; set; }
    public DbSet<SessionRecord> SessionRecords { get; set; }
    public DbSet<Signup> Signups { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }
    public DbSet<UserPhone> UserPhones { get; set; }
    public DbSet<UserSsn> UserSsns { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new AdminAuthConfiguration().Configure(builder.Entity<AdminAuth>());
        new BadSignInConfiguration().Configure(builder.Entity<BadSignIn>());
        new ClientConfiguration().Configure(builder.Entity<Client>());
        new ClientAuthConfiguration().Configure(builder.Entity<ClientAuth>());
        new ClientEmailAccountConfiguration().Configure(builder.Entity<ClientEmailAccount>());
        new ClientEmailMessageConfiguration().Configure(builder.Entity<ClientEmailMessage>());
        new ClientEventConfiguration().Configure(builder.Entity<ClientEvent>());
        new CustomerConfiguration().Configure(builder.Entity<Customer>());
        new EmailAttachmentConfiguration().Configure(builder.Entity<EmailAttachment>());
        new EmailImageConfiguration().Configure(builder.Entity<EmailImage>());
        new EmailMessageConfiguration().Configure(builder.Entity<EmailMessage>());
        new LogItemConfiguration().Configure(builder.Entity<LogItem>());
        new NetworkRequestConfiguration().Configure(builder.Entity<NetworkRequest>());
        new NetworkRuleConfiguration().Configure(builder.Entity<NetworkRule>());
        new SessionConfiguration().Configure(builder.Entity<Session>());
        new SessionActivityConfiguration().Configure(builder.Entity<SessionActivity>());
        new SessionRecordConfiguration().Configure(builder.Entity<SessionRecord>());
        new SignupConfiguration().Configure(builder.Entity<Signup>());
        new UserConfiguration().Configure(builder.Entity<User>());
        new UserEmailConfiguration().Configure(builder.Entity<UserEmail>());
        new UserEventConfiguration().Configure(builder.Entity<UserEvent>());
        new UserPhoneConfiguration().Configure(builder.Entity<UserPhone>());
        new UserSsnConfiguration().Configure(builder.Entity<UserSsn>());
    }

    public async Task SaveAsync(IUserToken userToken)
    {
        if (userToken.NoSave)
            throw new UserNoSaveException();

        if (!ChangeTracker.HasChanges())
            return;

        var entries = ChangeTracker.Entries();

        createdDateTimeSetter.SetCreated(entries);
        updatedDateTimeSetter.SetUpdated(entries);
        
        formatter.Format(entries);
        validator.Validate(entries);

        await base.SaveChangesAsync();
    }
}
