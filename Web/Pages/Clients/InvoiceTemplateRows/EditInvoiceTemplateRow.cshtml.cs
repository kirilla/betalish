using Betalish.Application.Commands.InvoiceTemplateRows.EditInvoiceTemplateRow;

namespace Betalish.Web.Pages.Clients.InvoiceTemplateRows;

public class EditInvoiceTemplateRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceTemplateRowCommand command) : ClientPageModel(userToken)
{
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;
    public InvoiceTemplateRow InvoiceTemplateRow { get; set; } = null!;

    [BindProperty]
    public EditInvoiceTemplateRowCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceTemplateRow = await database.InvoiceTemplateRows
                .Where(x =>
                    x.InvoiceTemplate.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceTemplateRow.InvoiceTemplateId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInvoiceTemplateRowCommandModel()
            {
                Id = InvoiceTemplateRow.Id,
                Quantity = InvoiceTemplateRow.Quantity
                    .ToString("N2", Swedish.CultureInfo),
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

            InvoiceTemplateRow = await database.InvoiceTemplateRows
                .Where(x =>
                    x.InvoiceTemplate.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceTemplateRow.InvoiceTemplateId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-template/{InvoiceTemplate.Id}");
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
}
