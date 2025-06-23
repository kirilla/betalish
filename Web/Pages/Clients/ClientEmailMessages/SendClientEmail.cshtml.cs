using Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class SendClientEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendClientEmailCommand command) : ClientPageModel(userToken)
{
    public ClientEmailMessage ClientEmailMessage { get; set; }

    [BindProperty]
    public SendClientEmailCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId, int id)
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

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();
            
            CommandModel = new SendClientEmailCommandModel()
            {
                Id = id,
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

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == CommandModel.Id &&
                    x.ClientId == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-client-desktop");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
