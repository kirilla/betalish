namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestUrlsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<string> URLs { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            URLs = await database.NetworkRequests
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
