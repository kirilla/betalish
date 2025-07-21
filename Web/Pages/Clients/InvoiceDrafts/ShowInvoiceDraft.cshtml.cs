namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class ShowInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; } = null!;
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public List<InvoiceDraftRow> InvoiceDraftRows { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraft.CustomerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceDraftRows = await database.InvoiceDraftRows
                .AsNoTracking()
                .Where(x =>
                    x.InvoiceDraft.ClientId == UserToken.ClientId!.Value &&
                    x.InvoiceDraftId == id)
                .OrderBy(x => x.ArticleNumber)
                .ThenBy(x => x.ArticleName)
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
