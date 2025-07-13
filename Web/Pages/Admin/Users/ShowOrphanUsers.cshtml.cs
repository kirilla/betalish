namespace Betalish.Web.Pages.Admin.Users;

public class ShowOrphanUsersModel(
    IDatabaseService database,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public List<User> Users { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            Users = await database.Users
                .AsNoTracking()
                .Where(x => 
                    !x.AdminAuths.Any() &&
                    !x.ClientAuths.Any())
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
