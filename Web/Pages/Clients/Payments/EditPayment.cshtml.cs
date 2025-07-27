using Betalish.Application.Commands.Payments.EditPayment;

namespace Betalish.Web.Pages.Clients.Payments;

public class EditPaymentModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPaymentCommand command) : ClientPageModel(userToken)
{
    public Payment Payment { get; set; } = null!;

    public List<PaymentAccount> PaymentAccounts { get; set; } = [];

    [BindProperty]
    public EditPaymentCommandModel CommandModel { get; set; }
        = new EditPaymentCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Payment = await database.Payments
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PaymentAccounts = await database.PaymentAccounts
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            CommandModel = new EditPaymentCommandModel()
            {
                Id = Payment.Id,
                Amount = Payment.Amount.ToSwedish(),
                Date = Payment.Date.ToIso8601(),
                PaymentMethod = Payment.PaymentMethod,
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

            Payment = await database.Payments
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            PaymentAccounts = await database.PaymentAccounts
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-payments");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
