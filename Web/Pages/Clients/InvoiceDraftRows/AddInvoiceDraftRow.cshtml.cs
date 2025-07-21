using Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

namespace Betalish.Web.Pages.Clients.InvoiceDraftRows;

public class AddInvoiceDraftRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInvoiceDraftRowCommand command) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public List<Article> Articles { get; set; } = [];

    [BindProperty]
    public AddInvoiceDraftRowCommandModel CommandModel { get; set; }
        = new AddInvoiceDraftRowCommandModel();

    public async Task<IActionResult> OnGet(int id)
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

            Articles = await database.Articles
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            CommandModel = new AddInvoiceDraftRowCommandModel()
            {
                InvoiceDraftId = id,
            };

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

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == CommandModel.InvoiceDraftId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Articles = await database.Articles
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{CommandModel.InvoiceDraftId}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
