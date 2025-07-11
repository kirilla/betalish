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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => x.Id == id)
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            ClientEmailAccount = await database.ClientEmailAccounts
                .Where(x => 
                    x.Id == CommandModel.ClientEmailAccountId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-email-account/{ClientEmailAccount.Id}");
        }
        catch (BlockedByNameException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.FromAddress),
                "Det finns ett annat konto med samma adress.");

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
