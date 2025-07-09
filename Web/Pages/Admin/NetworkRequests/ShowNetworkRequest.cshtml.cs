using Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequest;

namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class ShowNetworkRequestModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveNetworkRequestCommand removeNetworkRequestCommand) : AdminPageModel(userToken)
{
    public NetworkRequest NetworkRequest { get; set; }

    public bool CanRemoveNetworkRequest { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            NetworkRequest = await database.NetworkRequests
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CanRemoveNetworkRequest = 
                await removeNetworkRequestCommand.IsPermitted(userToken);

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
