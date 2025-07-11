using Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequests;

namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class RemoveNetworkRequestsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveNetworkRequestsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveNetworkRequestsCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveNetworkRequestsCommandModel();

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-network-requests");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
