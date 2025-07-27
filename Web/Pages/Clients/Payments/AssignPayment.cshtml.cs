using Betalish.Application.Commands.Payments.AssignPayment;

namespace Betalish.Web.Pages.Clients.Payments;

public class AssignPaymentModel(
    IUserToken userToken,
    IDatabaseService database,
    IAssignPaymentCommand command) : ClientPageModel(userToken)
{
    public Payment Payment { get; set; } = null!;

    public List<Invoice> Invoices { get; set; } = [];

    [BindProperty]
    public AssignPaymentCommandModel CommandModel { get; set; }
        = new AssignPaymentCommandModel();

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

            Invoices = await database.Invoices
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            CommandModel = new AssignPaymentCommandModel()
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

            Invoices = await database.Invoices
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment/{Payment.Id}");
        }
        catch (AlreadyAssignedException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.InvoiceId),
                "Betalningen är redan knuten till en faktura.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
