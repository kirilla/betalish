namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class ShowInvoiceRangeModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public InvoiceRange InvoiceRange { get; set; } = null!;

    public int InvoiceCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceRange = await database.InvoiceRanges
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceCount = await database.Invoices
                .CountAsync(x => 
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.InvoiceNumber >= InvoiceRange.StartNumber &&
                    x.InvoiceNumber <= InvoiceRange.EndNumber);

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
