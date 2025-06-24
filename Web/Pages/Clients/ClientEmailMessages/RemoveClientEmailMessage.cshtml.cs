using Betalish.Application.Commands.ClientEmailMessages.RemoveClientEmailMessage;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class RemoveClientEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveClientEmailMessageCommand command) : ClientPageModel(userToken)
{
    public ClientEmailMessage ClientEmailMessage { get; set; }

    [BindProperty]
    public RemoveClientEmailMessageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertClientAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveClientEmailMessageCommandModel()
            {
                Id = ClientEmailMessage.Id,
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await AssertClientAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == CommandModel.Id &&
                    x.ClientId == Client.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-client-desktop");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort meddelandet.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
