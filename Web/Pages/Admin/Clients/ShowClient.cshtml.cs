namespace Betalish.Web.Pages.Admin.Clients;

public class ShowClientModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public Client Client { get; set; } = null!;

    public List<User> Users { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            Client = await database.Clients
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Users = await database.ClientAuths
                .Where(x => x.ClientId == id)
                .Select(x => x.User)
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
