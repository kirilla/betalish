using Betalish.Application.Commands.PaymentAccounts.AddPaymentAccount;

namespace Betalish.Web.Pages.Clients.PaymentAccounts;

public class AddPaymentAccountModel(
    IUserToken userToken,
    IAddPaymentAccountCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddPaymentAccountCommandModel CommandModel { get; set; }
        = new AddPaymentAccountCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPaymentAccountCommandModel();

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

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment-accounts");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns redan ett konto med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
