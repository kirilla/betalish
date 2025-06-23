using Betalish.Application.Commands.ClientEmailAccounts.EditClientEmailAccount;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class EditClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditClientEmailAccountCommand command) : ClientPageModel(userToken)
{
    public ClientEmailAccount ClientEmailAccount { get; set; }

    [BindProperty]
    public EditClientEmailAccountCommandModel CommandModel { get; set; }

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

            CommandModel = new EditClientEmailAccountCommandModel()
            {
                ClientEmailAccountId = ClientEmailAccount.Id,
                FromName = ClientEmailAccount.FromName,
                FromAddress = ClientEmailAccount.FromAddress,
                ReplyToName = ClientEmailAccount.ReplyToName,
                ReplyToAddress = ClientEmailAccount.ReplyToAddress,
                Password = ClientEmailAccount.Password,
                SmtpHost = ClientEmailAccount.SmtpHost,
                SmtpPort = ClientEmailAccount.SmtpPort,
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

    public async Task<IActionResult> OnPostAsync(int clientId, int clientEmailAccountId)
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
                .Where(x => 
                    x.Id == CommandModel.ClientEmailAccountId &&
                    x.ClientId == clientId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel, clientId);

            return Redirect($"/client/{clientId}/show-client-email-account/{clientEmailAccountId}");
        }
        catch (BlockedByNameException)
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
