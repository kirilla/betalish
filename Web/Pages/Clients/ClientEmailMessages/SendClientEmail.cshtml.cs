using Betalish.Application.Commands.ClientEmailMessages.SendClientEmail;

namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class SendClientEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendClientEmailCommand command) : ClientPageModel(userToken)
{
    public ClientEmailMessage ClientEmailMessage { get; set; } = null!;

    [BindProperty]
    public SendClientEmailCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsClient();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            AssertIsClient();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ClientEmailMessage = await database.ClientEmailMessages
                .Where(x => 
                    x.Id == CommandModel.Id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-desktop");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
