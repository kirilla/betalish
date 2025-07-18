using Betalish.Application.Commands.InvoiceTemplates.EditInvoiceTemplate;

namespace Betalish.Web.Pages.Clients.InvoiceTemplates;

public class EditInvoiceTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceTemplateCommand command) : ClientPageModel(userToken)
{
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;

    [BindProperty]
    public EditInvoiceTemplateCommandModel CommandModel { get; set; }
        = new EditInvoiceTemplateCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInvoiceTemplateCommandModel()
            {
                Id = InvoiceTemplate.Id,
                //TODO = InvoiceTemplate.TODO,
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

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-template/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
