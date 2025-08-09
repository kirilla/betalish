using Betalish.Application.Commands.EmailAccounts.SetEmailAccountPassword;

namespace Betalish.Web.Pages.Clients.EmailAccounts;

public class SetEmailAccountPasswordModel(
    IUserToken userToken,
    IDatabaseService database,
    ISetEmailAccountPasswordCommand command) : ClientPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; } = null!;

    [BindProperty]
    public SetEmailAccountPasswordCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new SetEmailAccountPasswordCommandModel()
            {
                Id = EmailAccount.Id,
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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x => 
                    x.Id == CommandModel.Id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-account/{EmailAccount.Id}");
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
