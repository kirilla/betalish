namespace Betalish.Web.Pages.Clients.Invoices;

public class ShowInvoiceModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    public List<InvoiceRow> InvoiceRows { get; set; } = [];

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
