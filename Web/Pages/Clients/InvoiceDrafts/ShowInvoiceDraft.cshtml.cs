namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class ShowInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public PaymentTerms? BillingStrategy { get; set; } = null!;

    public List<InvoiceDraftRow> InvoiceDraftRows { get; set; } = [];
    public List<DraftBalanceRow> DraftBalanceRows { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            BillingStrategy = await database.PaymentTerms
                .Where(x =>
                    x.Id == InvoiceDraft.BillingStrategyId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync();

            InvoiceDraftRows = await database.InvoiceDraftRows
                .AsNoTracking()
                .Where(x =>
                    x.InvoiceDraftId == id &&
                    x.InvoiceDraft.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.ArticleNumber)
                .ThenBy(x => x.ArticleName)
                .ToListAsync();

            DraftBalanceRows = await database.DraftBalanceRows
                .Where(x => 
                    x.InvoiceDraftId == id &&
                    x.InvoiceDraft.ClientId == UserToken.ClientId!.Value)
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
