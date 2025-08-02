namespace Betalish.Web.Pages.Clients.Batches;

public class ShowBatchesModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<BatchSummary> Batches { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Batches = await database.Batches
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Id)
                .Select(x => new BatchSummary { 
                    Id = x.Id,
                    Name = x.Name,
                    InvoiceCount = x.Invoices.Count,
                    InvoiceDraftCount = x.InvoiceDrafts.Count,
                })
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
