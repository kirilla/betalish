using Betalish.Application.Commands.InvoiceTemplates.AddInvoiceTemplate;

namespace Betalish.Web.Pages.Clients.InvoiceTemplates;

public class AddInvoiceTemplateModel(
    IUserToken userToken,
    IAddInvoiceTemplateCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddInvoiceTemplateCommandModel CommandModel { get; set; }
        = new AddInvoiceTemplateCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddInvoiceTemplateCommandModel();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-template/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
