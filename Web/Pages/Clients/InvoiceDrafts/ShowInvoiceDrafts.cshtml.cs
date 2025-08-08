namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class ShowInvoiceDraftsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceDrafts = await database.InvoiceDrafts
                .AsNoTracking()
                .Include(x => x.Batch)
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.InvoiceDate)
                .ThenBy(x => x.Customer_Name)
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
