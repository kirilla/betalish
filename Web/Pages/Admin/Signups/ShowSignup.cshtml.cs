namespace Betalish.Web.Pages.Admin.Signups;

public class SignupModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public Signup Signup { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            Signup = await database.Signups
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
