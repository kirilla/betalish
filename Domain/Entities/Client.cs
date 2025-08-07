namespace Betalish.Domain.Entities;

public class Client
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Address { get; set; }

    public List<Article> Articles { get; set; } = [];
    public List<Batch> Batches { get; set; } = [];
    public List<ClientAuth> ClientAuths { get; set; } = [];
    public List<ClientEmailAccount> ClientEmailAccounts { get; set; } = [];
    public List<ClientEmailMessage> ClientEmailMessages { get; set; } = [];
    public List<ClientEvent> ClientEvents { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
    public List<Invoice> Invoices { get; set; } = [];
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];
    public List<InvoiceRange> InvoiceRanges { get; set; } = [];
    public List<InvoiceTemplate> InvoiceTemplates { get; set; } = [];
    public List<LedgerAccount> LedgerAccounts { get; set; } = [];
    public List<Payment> Payments { get; set; } = [];
    public List<PaymentAccount> PaymentAccounts { get; set; } = [];
    public List<PaymentTerms> PaymentTerms { get; set; } = [];
    public List<Session> Sessions { get; set; } = [];
    public List<SessionRecord> SessionRecords { get; set; } = [];
}
