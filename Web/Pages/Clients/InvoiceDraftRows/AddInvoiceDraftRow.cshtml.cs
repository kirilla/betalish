using Betalish.Application.Commands.InvoiceDraftRows.AddInvoiceDraftRow;

namespace Betalish.Web.Pages.Clients.InvoiceDraftRows;

public class AddInvoiceDraftRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInvoiceDraftRowCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddInvoiceDraftRowCommandModel CommandModel { get; set; }
        = new AddInvoiceDraftRowCommandModel();

    public List<Article> Articles { get; set; } = [];

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

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

            Articles = await database.Articles
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{CommandModel.InvoiceDraftId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.ArticleId),
                "Det finns redan en rad för artikeln.");

            return Page();
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
