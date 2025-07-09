using Betalish.Application.Commands.NetworkRequests.RemoveNetworkRequest;

namespace Betalish.Web.Pages.Admin.NetworkRequests;

public class RemoveNetworkRequestModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveNetworkRequestCommand command) : AdminPageModel(userToken)
{
    public NetworkRequest NetworkRequest { get; set; }

    [BindProperty]
    public RemoveNetworkRequestCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            NetworkRequest = await database.NetworkRequests
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveNetworkRequestCommandModel()
            {
                Id = NetworkRequest.Id,
            };

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
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            NetworkRequest = await database.NetworkRequests
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-network-requests");
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
