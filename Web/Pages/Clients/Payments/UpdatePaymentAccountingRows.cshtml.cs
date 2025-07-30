using Betalish.Application.Commands.Payments.UpdatePaymentAccountingRows;

namespace Betalish.Web.Pages.Clients.Payments;

public class UpdatePaymentAccountingRowsModel(
    IUserToken userToken,
    IDatabaseService database,
    IUpdatePaymentAccountingRowsCommand command) : ClientPageModel(userToken)
{
    public Payment Payment { get; set; } = null!;

    [BindProperty]
    public UpdatePaymentAccountingRowsCommandModel CommandModel { get; set; } = new();

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

            CommandModel = new UpdatePaymentAccountingRowsCommandModel()
            {
                Id = Payment.Id,
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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment/{id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill omskapa konteringen.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
