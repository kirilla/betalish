using Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class RemoveClientEmailMessagesModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveClientEmailMessagesCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public RemoveClientEmailMessagesCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertClientAuthorization(database);

            if (!await command.IsPermitted(UserToken))
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
            await AssertClientAuthorization(database);

            if (!await command.IsPermitted(UserToken))
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
