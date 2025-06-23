using Betalish.Application.Commands.ClientEmailAccounts.AddClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class AddClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddClientEmailAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddClientEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new AddClientEmailAccountCommandModel()
            {
                ClientId = clientId,
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
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel, CommandModel.ClientId);

            return Redirect($"/client/{clientId}/show-client-email-account/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.FromAddress),
                "Det finns ett annat epostkonto med samma adress.");

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
}
