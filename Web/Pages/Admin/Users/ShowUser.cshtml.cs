namespace Betalish.Web.Pages.Admin.Users;

public class ShowUserModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    public List<Client> Clients { get; set; }
    public List<UserEmail> UserEmails { get; set; }

    public bool IsAdmin { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            User = await database.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            UserEmails = await database.UserEmails
                .Where(x => x.UserId == id)
                .ToListAsync();

            Clients = await database.ClientAuths
                .Where(x => x.UserId == id)
                .Select(x => x.Client)
                .OrderBy(x => x.Name)
                .ToListAsync();

            IsAdmin = await database.AdminAuths.AnyAsync(x => x.UserId == id);

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
