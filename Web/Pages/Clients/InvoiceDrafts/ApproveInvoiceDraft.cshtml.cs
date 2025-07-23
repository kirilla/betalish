using Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class ApproveInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IApproveInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; } = null!;
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    [BindProperty]
    public ApproveInvoiceDraftCommandModel CommandModel { get; set; }
        = new ApproveInvoiceDraftCommandModel();

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

            CommandModel = new ApproveInvoiceDraftCommandModel()
            {
                Id = InvoiceDraft.Id,
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

            var invoiceId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice/{invoiceId}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
