using Betalish.Application.Commands.ClientEvents.RemoveClientEvents;

namespace Betalish.Web.Pages.Admin.ClientEvents;

public class RemoveClientEventsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveClientEventsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveClientEventsCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveClientEventsCommandModel();

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

            return Redirect("/show-client-events");
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
