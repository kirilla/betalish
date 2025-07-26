using Betalish.Application.Commands.Payments.AddPayment;

namespace Betalish.Web.Pages.Clients.Payments;

public class AddPaymentModel(
    IUserToken userToken,
    IAddPaymentCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddPaymentCommandModel CommandModel { get; set; }
        = new AddPaymentCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPaymentCommandModel();

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

            return Redirect($"/show-payment/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
