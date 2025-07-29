using Betalish.Application.Commands.Payments.UnassignPayment;

namespace Betalish.Web.Pages.Clients.Payments;

public class UnassignPaymentModel(
    IUserToken userToken,
    IDatabaseService database,
    IUnassignPaymentCommand command) : ClientPageModel(userToken)
{
    public Payment Payment { get; set; } = null!;
    public Invoice? Invoice { get; set; } = null!;

    [BindProperty]
    public UnassignPaymentCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Payment = await database.Payments
                .Include(x => x.PaymentAccount)
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == Payment.InvoiceId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync();

            CommandModel = new UnassignPaymentCommandModel()
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
                .Include(x => x.PaymentAccount)
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == Payment.InvoiceId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotAssignedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment/{Payment.Id}");
        }
        catch (NotAssignedException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Id),
                "Betalningen är inte knuten till någon faktura.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
