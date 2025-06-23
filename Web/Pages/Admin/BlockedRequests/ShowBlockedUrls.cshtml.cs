namespace Betalish.Web.Pages.Admin.BlockedRequests;

public class ShowBlockedUrlsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<string> UserAgents { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            UserAgents = await database.BlockedRequests
                .AsNoTracking()
                .Select(x => x.Url)
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
