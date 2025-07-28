namespace Betalish.Web.Pages.Clients.Invoices;

public class ShowInvoiceModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    public List<InvoiceAccounting> InvoiceAccountings { get; set; } = [];
    public List<InvoiceRow> InvoiceRows { get; set; } = [];
    public List<BalanceRow> BalanceRows { get; set; } = [];
    public List<DraftBalanceRow> DraftBalanceRows { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Invoice = await database.Invoices
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceRows = await database.InvoiceRows
                .AsNoTracking()
                .Where(x =>
                    x.Invoice.ClientId == UserToken.ClientId!.Value &&
                    x.InvoiceId == id)
                .OrderBy(x => x.ArticleNumber)
                .ThenBy(x => x.ArticleName)
                .ToListAsync();

            InvoiceAccountings = await database.InvoiceAccountings
                .AsNoTracking()
                .Where(x =>
                    x.InvoiceId == id &&
                    x.Invoice.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Account)
                .ToListAsync();

            BalanceRows = await database.BalanceRows
                .Include(x => x.CreditInvoice)
                .Include(x => x.DebitInvoice)
                .Where(x =>
                    x.CreditInvoiceId == id ||
                    x.DebitInvoiceId == id)
                .ToListAsync();

            DraftBalanceRows = await database.DraftBalanceRows
                .Where(x => x.InvoiceId == id)
                .ToListAsync();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
