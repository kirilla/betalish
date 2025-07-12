using Betalish.Application.Commands.UserPhones.AddAccountPhone;

namespace Betalish.Web.Pages.Account.AccountPhones;

public class AddAccountPhoneModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddAccountPhoneCommand command) : UserTokenPageModel(userToken)
{
    [BindProperty]
    public AddAccountPhoneCommandModel CommandModel { get; set; }
        = new AddAccountPhoneCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddAccountPhoneCommandModel();

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

            return Redirect($"/show-account");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Number),
                "Numret finns redan.");

            return Page();
        }
        catch (TooManyException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Number),
                $"Max {Limits.User.PhoneNumbers.Max} nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
