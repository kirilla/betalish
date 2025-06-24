using Betalish.Application.Commands.Sessions.EndOtherSessions;

namespace Betalish.Web.Pages.Admin.Sessions;

public class EndOtherSessionsModel(
    IUserToken userToken,
    IDatabaseService database,
    IEndOtherSessionsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public EndOtherSessionsCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new EndOtherSessionsCommandModel();

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
