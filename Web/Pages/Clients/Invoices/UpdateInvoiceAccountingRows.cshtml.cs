using Betalish.Application.Commands.Invoices.UpdateInvoiceAccountingRows;

namespace Betalish.Web.Pages.Clients.Invoices;

public class UpdateInvoiceAccountingRowsModel(
    IUserToken userToken,
    IDatabaseService database,
    IUpdateInvoiceAccountingRowsCommand command) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    [BindProperty]
    public UpdateInvoiceAccountingRowsCommandModel CommandModel { get; set; } = new();

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

            CommandModel = new UpdateInvoiceAccountingRowsCommandModel()
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
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
