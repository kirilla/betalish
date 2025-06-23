namespace Betalish.Web.Pages.Admin.BlockedRequests;

public class ShowBlockedRequestsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<BlockedRequest> BlockedRequests { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            BlockedRequests = await database.BlockedRequests
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
