using Betalish.Application.Commands.BlockedRequests.RemoveBlockedRequest;

namespace Betalish.Web.Pages.Admin.BlockedRequests;

public class RemoveBlockedRequestModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveBlockedRequestCommand command) : AdminPageModel(userToken)
{
    public BlockedRequest BlockedRequest { get; set; }

    [BindProperty]
    public RemoveBlockedRequestCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BlockedRequest = await database.BlockedRequests
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveBlockedRequestCommandModel()
            {
                BlockedRequestId = BlockedRequest.Id,
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

            BlockedRequest = await database.BlockedRequests
                .Where(x => x.Id == CommandModel.BlockedRequestId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-blocked-requests");
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
