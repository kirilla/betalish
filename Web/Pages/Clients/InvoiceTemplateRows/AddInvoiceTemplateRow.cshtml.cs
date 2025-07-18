using Betalish.Application.Commands.InvoiceTemplateRows.AddInvoiceTemplateRow;

namespace Betalish.Web.Pages.Clients.InvoiceTemplateRows;

public class AddInvoiceTemplateRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInvoiceTemplateRowCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddInvoiceTemplateRowCommandModel CommandModel { get; set; }
        = new AddInvoiceTemplateRowCommandModel();

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

            CommandModel = new AddInvoiceTemplateRowCommandModel()
            {
                InvoiceTemplateId = id,
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

            return Redirect($"/show-invoice-template/{CommandModel.InvoiceTemplateId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.ArticleId),
                "Det finns redan en rad för artikeln.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
