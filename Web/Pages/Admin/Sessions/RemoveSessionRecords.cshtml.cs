using Betalish.Application.Commands.SessionRecords.RemoveSessionRecords;

namespace Betalish.Web.Pages.Admin.Sessions;

public class RemoveSessionRecordsModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveSessionRecordsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveSessionRecordsCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveSessionRecordsCommandModel();

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

            return Redirect("/show-session-records");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
