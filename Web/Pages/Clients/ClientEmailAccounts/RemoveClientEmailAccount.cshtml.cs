using Betalish.Application.Commands.ClientEmailAccounts.RemoveClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class RemoveClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveClientEmailAccountCommand command) : ClientPageModel(userToken)
{
    public ClientEmailAccount ClientEmailAccount { get; set; }

    [BindProperty]
    public RemoveClientEmailAccountCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int clientId, int clientEmailAccountId)
    {
        try
        {
            if (!await command.IsPermitted(UserToken, clientId))
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => x.Id == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => x.Id == clientEmailAccountId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveClientEmailAccountCommandModel()
            {
                ClientEmailAccountId = ClientEmailAccount.Id,
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

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => x.Id == CommandModel.ClientEmailAccountId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-client-email-accounts");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

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
