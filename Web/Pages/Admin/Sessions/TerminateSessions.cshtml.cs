using Betalish.Application.Commands.Sessions.TerminateSessions;

namespace Betalish.Web.Pages.Admin.Sessions;

public class TerminateSessionsModel(
    IUserToken userToken,
    ITerminateSessionsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public TerminateSessionsCommandModel CommandModel { get; set; }
        = new TerminateSessionsCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new TerminateSessionsCommandModel();

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

            return Redirect("/show-all-sessions");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
