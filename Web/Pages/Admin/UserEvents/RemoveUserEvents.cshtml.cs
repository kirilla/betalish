using Betalish.Application.Commands.UserEvents.RemoveUserEvents;

namespace Betalish.Web.Pages.Admin.UserEvents;

public class RemoveUserEventsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveUserEventsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveUserEventsCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveUserEventsCommandModel();

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

            return Redirect("/show-user-events");
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
