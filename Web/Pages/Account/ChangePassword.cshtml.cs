using Betalish.Application.Commands.Account.ChangePassword;

namespace Betalish.Web.Pages.Account;

public class ChangePasswordModel(
    IUserToken userToken,
    IChangePasswordCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public ChangePasswordCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new ChangePasswordCommandModel();

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/help/success");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
