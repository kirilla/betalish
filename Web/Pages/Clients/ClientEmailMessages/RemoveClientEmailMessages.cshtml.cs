using Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessages;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class RemoveClientEmailMessagesModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveClientEmailMessagesCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public RemoveClientEmailMessagesCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            await AssertClientAuthorization(database, clientId);

            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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

    public async Task<IActionResult> OnPostAsync(int clientId)
    {
        try
        {
            await AssertClientAuthorization(database, clientId);

            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-client-email-queue");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
