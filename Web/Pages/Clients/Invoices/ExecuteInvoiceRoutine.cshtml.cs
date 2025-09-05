using Betalish.Application.Commands.Invoices.ExecuteInvoiceRoutine;

namespace Betalish.Web.Pages.Clients.Invoices;

public class ExecuteInvoiceRoutineModel(
    IUserToken userToken,
    IDatabaseService database,
    IExecuteInvoiceRoutineCommand command) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    [BindProperty]
    public ExecuteInvoiceRoutineCommandModel CommandModel { get; set; } = new();

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

            CommandModel = new ExecuteInvoiceRoutineCommandModel()
            {
                InvoiceId = Invoice.Id,
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
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
