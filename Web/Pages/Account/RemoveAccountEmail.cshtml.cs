using Betalish.Application.Commands.UserEmails.RemoveAccountEmail;

namespace Betalish.Web.Pages.Account;

public class RemoveAccountEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAccountEmailCommand command) : UserTokenPageModel(userToken)
{
    public List<UserEmail> UserEmails { get; set; }

    [BindProperty]
    public RemoveAccountEmailCommandModel CommandModel { get; set; } = new RemoveAccountEmailCommandModel();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            UserEmails = await database.UserEmails
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            CommandModel = new RemoveAccountEmailCommandModel();

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

            UserEmails = await database.UserEmails
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-account");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort adressen.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
