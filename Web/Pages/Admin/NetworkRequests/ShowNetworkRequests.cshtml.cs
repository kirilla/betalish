namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<NetworkRequest> NetworkRequests { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            NetworkRequests = await database.NetworkRequests
                .AsNoTracking()
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
