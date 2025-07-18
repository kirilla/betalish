using Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class EditInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; } = null!;
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    [BindProperty]
    public EditInvoiceDraftCommandModel CommandModel { get; set; }
        = new EditInvoiceDraftCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraft.CustomerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInvoiceDraftCommandModel()
            {
                Id = InvoiceDraft.Id,
                About = InvoiceDraft.About,
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

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraft.CustomerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
