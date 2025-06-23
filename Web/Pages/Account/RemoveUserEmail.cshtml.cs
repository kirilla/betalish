using Betalish.Application.Commands.UserEmails.RemoveUserEmail;

namespace Betalish.Web.Pages.Account;

public class RemoveUserEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveUserEmailCommand command) : UserTokenPageModel(userToken)
{
    public List<UserEmail> UserEmails { get; set; }

    [BindProperty]
    public RemoveUserEmailCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            UserEmails = await database.UserEmails
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            CommandModel = new RemoveUserEmailCommandModel();

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

            return Redirect($"/account/show-account");
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
