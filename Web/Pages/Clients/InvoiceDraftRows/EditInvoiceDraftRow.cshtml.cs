using Betalish.Application.Commands.InvoiceDraftRows.EditInvoiceDraftRow;

namespace Betalish.Web.Pages.Clients.InvoiceDraftRows;

public class EditInvoiceDraftRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceDraftRowCommand command) : ClientPageModel(userToken)
{
    public Article Article { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public InvoiceDraft InvoiceDraft { get; set; } = null!;
    public InvoiceDraftRow InvoiceDraftRow { get; set; } = null!;

    [BindProperty]
    public EditInvoiceDraftRowCommandModel CommandModel { get; set; }
        = new EditInvoiceDraftRowCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceDraftRow = await database.InvoiceDraftRows
                .Where(x =>
                    x.InvoiceDraft.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraftRow.InvoiceDraftId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraft.CustomerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Article = await database.Articles
                .Where(x => 
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraftRow.ArticleId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInvoiceDraftRowCommandModel()
            {
                Id = InvoiceDraftRow.Id,
                ArticleName = InvoiceDraftRow.ArticleName,
                Quantity = InvoiceDraftRow.Quantity
                    .ToString("N2", Swedish.CultureInfo),
                UnitPrice = InvoiceDraftRow.UnitPrice
                    .ToString("N2", Swedish.CultureInfo),
                RevenueAccount = InvoiceDraftRow.RevenueAccount,
                VatAccount = InvoiceDraftRow.VatAccount,
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

            InvoiceDraftRow = await database.InvoiceDraftRows
                .Where(x =>
                    x.InvoiceDraft.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraftRow.InvoiceDraftId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Customer = await database.Customers
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraft.CustomerId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Article = await database.Articles
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceDraftRow.ArticleId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{InvoiceDraft.Id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
