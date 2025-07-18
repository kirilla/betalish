namespace Betalish.Web.Pages.Clients.InvoiceTemplates;

public class ShowInvoiceTemplateModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;

    public List<InvoiceTemplateRow> InvoiceTemplateRows { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceTemplateRows = await database.InvoiceTemplateRows
                .AsNoTracking()
                .Include(x => x.Article)
                .Where(x =>
                    x.InvoiceTemplate.ClientId == UserToken.ClientId!.Value &&
                    x.InvoiceTemplateId == id)
                .OrderBy(x => x.Article.Number)
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
