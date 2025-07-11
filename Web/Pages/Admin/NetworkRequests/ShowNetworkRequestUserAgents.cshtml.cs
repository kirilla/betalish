namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestUserAgentsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<string> UserAgents { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            UserAgents = await database.NetworkRequests
                .AsNoTracking()
                .Select(x => x.UserAgent)
                .Where(x => x != null)
                .Cast<string>()
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
