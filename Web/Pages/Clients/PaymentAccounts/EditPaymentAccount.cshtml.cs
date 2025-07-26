using Betalish.Application.Commands.PaymentAccounts.EditPaymentAccount;

namespace Betalish.Web.Pages.Clients.PaymentAccounts;

public class EditPaymentAccountModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPaymentAccountCommand command) : ClientPageModel(userToken)
{
    public PaymentAccount PaymentAccount { get; set; } = null!;

    [BindProperty]
    public EditPaymentAccountCommandModel CommandModel { get; set; }
        = new EditPaymentAccountCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PaymentAccount = await database.PaymentAccounts
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditPaymentAccountCommandModel()
            {
                Id = PaymentAccount.Id,
                Name = PaymentAccount.Name,
                Description = PaymentAccount.Description,
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
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-payment-accounts");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns ett annat konto med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
