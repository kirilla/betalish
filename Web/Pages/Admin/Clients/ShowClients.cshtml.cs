namespace Betalish.Web.Pages.Admin.Clients;

public class ShowClientsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<Client> Clients { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            Clients = await database.Clients
                .AsNoTracking()
                .OrderBy(x => x.Address)
                .ThenBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
