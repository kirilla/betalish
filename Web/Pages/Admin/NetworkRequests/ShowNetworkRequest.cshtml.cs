namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public NetworkRequest NetworkRequest { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            NetworkRequest = await database.NetworkRequests
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
