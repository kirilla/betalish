using Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class RemoveClientEmailMessagesModel(
    IUserToken userToken,
    IRemoveClientEmailMessagesCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public RemoveClientEmailMessagesCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            AssertIsClient();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new RemoveClientEmailMessagesCommandModel();

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
            AssertIsClient();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-email-queue");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
