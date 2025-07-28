using Betalish.Application.Commands.Invoices.UpdateInvoiceAccounting;

namespace Betalish.Web.Pages.Clients.Invoices;

public class UpdateInvoiceAccountingModel(
    IUserToken userToken,
    IDatabaseService database,
    IUpdateInvoiceAccountingCommand command) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    [BindProperty]
    public UpdateInvoiceAccountingCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new UpdateInvoiceAccountingCommandModel()
            {
                Id = Invoice.Id,
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

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice/{id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill omskapa konteringen.");

            return Page();
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
