namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class ShowInvoiceRangesModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<InvoiceRange> InvoiceRanges { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            InvoiceRanges = await database.InvoiceRanges
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.EffectiveStartDate)
                .ThenBy(x => x.EffectiveEndDate)
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
