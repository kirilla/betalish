using Betalish.Application.Commands.PaymentAccounts.RemovePaymentAccount;

namespace Betalish.Web.Pages.Clients.PaymentAccounts;

public class RemovePaymentAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemovePaymentAccountCommand command) : ClientPageModel(userToken)
{
    public PaymentAccount PaymentAccount { get; set; } = null!;

    [BindProperty]
    public RemovePaymentAccountCommandModel CommandModel { get; set; }
        = new RemovePaymentAccountCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PaymentAccount = await database.PaymentAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemovePaymentAccountCommandModel()
            {
                Id = PaymentAccount.Id,
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

            PaymentAccount = await database.PaymentAccounts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment-accounts");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
