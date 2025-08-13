using Betalish.Application.Commands.EmailAccounts.TestEmailAccount;

namespace Betalish.Web.Pages.Clients.EmailAccounts;

public class TestEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    ITestEmailAccountCommand command) : ClientPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; } = null!;

    [BindProperty]
    public TestEmailAccountCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new TestEmailAccountCommandModel()
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailAccount = await database.EmailAccounts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-email-account/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
