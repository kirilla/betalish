namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomersModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<Customer> Customers { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Ssn10)
                .ThenBy(x => x.Orgnum)
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
