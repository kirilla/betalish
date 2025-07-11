using Betalish.Application.Commands.ClientEmailAccounts.SetClientEmailAccountPassword;

namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class SetClientEmailAccountPasswordModel(
    IUserToken userToken,
    IDatabaseService database,
    ISetClientEmailAccountPasswordCommand command) : ClientPageModel(userToken)
{
    public ClientEmailAccount ClientEmailAccount { get; set; }

    [BindProperty]
    public SetClientEmailAccountPasswordCommandModel CommandModel { get; set; }

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

            CommandModel = new SetClientEmailAccountPasswordCommandModel()
            {
                Id = ClientEmailAccount.Id,
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
                    x.Id == CommandModel.Id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-email-account/{ClientEmailAccount.Id}");
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
