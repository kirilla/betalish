using Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class AddInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddInvoiceDraftCommandModel CommandModel { get; set; }
        = new AddInvoiceDraftCommandModel();

    public List<Customer> Customers { get; set; } = [];
    public List<InvoiceTemplate> InvoiceTemplates { get; set; } = [];

    public async Task<IActionResult> OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            InvoiceTemplates = await database.InvoiceTemplates
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            CommandModel = new AddInvoiceDraftCommandModel();

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

            Customers = await database.Customers
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            InvoiceTemplates = await database.InvoiceTemplates
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
