namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestIpAddressesModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<string> IpAddresses { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            IpAddresses = await database.NetworkRequests
                .AsNoTracking()
                .Select(x => x.IpAddress)
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
