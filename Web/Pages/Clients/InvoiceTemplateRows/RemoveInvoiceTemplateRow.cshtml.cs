using Betalish.Application.Commands.InvoiceTemplateRows.RemoveInvoiceTemplateRow;

namespace Betalish.Web.Pages.Clients.InvoiceTemplateRows;

public class RemoveInvoiceTemplateRowModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInvoiceTemplateRowCommand command) : ClientPageModel(userToken)
{
    public Article Article { get; set; } = null!;
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;
    public InvoiceTemplateRow InvoiceTemplateRow { get; set; } = null!;

    [BindProperty]
    public RemoveInvoiceTemplateRowCommandModel CommandModel { get; set; }
        = new RemoveInvoiceTemplateRowCommandModel();

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

            Article = await database.Articles
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceTemplateRow.ArticleId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveInvoiceTemplateRowCommandModel()
            {
                Id = InvoiceTemplateRow.Id,
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

            Article = await database.Articles
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == InvoiceTemplateRow.ArticleId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-template/{InvoiceTemplate.Id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
