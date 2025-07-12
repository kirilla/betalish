using Betalish.Application.Commands.EmailMessages.RemoveEmailMessages;

namespace Betalish.Web.Pages.Admin.EmailMessages;

public class RemoveEmailMessagesModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailMessagesCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveEmailMessagesCommandModel CommandModel { get; set; } 
        = new RemoveEmailMessagesCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveEmailMessagesCommandModel();

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

            return Redirect("/show-email-queue");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
