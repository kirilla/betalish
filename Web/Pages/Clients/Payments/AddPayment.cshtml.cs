using Betalish.Application.Commands.Payments.AddPayment;

namespace Betalish.Web.Pages.Clients.Payments;

public class AddPaymentModel(
    IUserToken userToken,
    IDatabaseService database,
    IDateService dateService,
    IAddPaymentCommand command) : ClientPageModel(userToken)
{
    public List<PaymentAccount> PaymentAccounts { get; set; } = [];

    [BindProperty]
    public AddPaymentCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PaymentAccounts = await database.PaymentAccounts
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            var today = dateService.GetDateOnlyToday();

            CommandModel = new AddPaymentCommandModel()
            {
                Date = today.ToString("yyyy-MM-"),
            };

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

            PaymentAccounts = await database.PaymentAccounts
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

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
