namespace Betalish.Web.Pages.Clients.Batches;

public class ShowBatchModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Batch Batch { get; set; } = null!;

    public List<Invoice> Invoices { get; set; } = [];
    public List<InvoiceDraft> InvoiceDrafts { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Batch = await database.Batches
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Invoices = await database.Invoices
                .AsNoTracking()
                .Where(x =>
                    x.BatchId == id &&
                    x.Batch!.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.InvoiceDate)
                .ToListAsync();

            InvoiceDrafts = await database.InvoiceDrafts
                .AsNoTracking()
                .Where(x =>
                    x.BatchId == id &&
                    x.Batch!.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.InvoiceDate)
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
