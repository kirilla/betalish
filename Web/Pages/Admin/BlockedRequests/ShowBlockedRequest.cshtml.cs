using Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

namespace Betalish.Web.Pages.Admin.BlockedRequests;

public class ShowBlockedRequestModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveBlockedRequestCommand removeBlockedRequestCommand) : AdminPageModel(userToken)
{
    public BlockedRequest BlockedRequest { get; set; }

    public bool CanRemoveBlockedRequest { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            BlockedRequest = await database.BlockedRequests
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanRemoveBlockedRequest = 
                await removeBlockedRequestCommand.IsPermitted(userToken);

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
