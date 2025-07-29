using Betalish.Application.Commands.Articles.EditArticleAccounting;

namespace Betalish.Web.Pages.Clients.Articles;

public class EditArticleAccountingModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditArticleAccountingCommand command) : ClientPageModel(userToken)
{
    public Article Article { get; set; } = null!;

    [BindProperty]
    public EditArticleAccountingCommandModel CommandModel { get; set; }
        = new EditArticleAccountingCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Article = await database.Articles
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditArticleAccountingCommandModel()
            {
                Id = Article.Id,
                RevenueAccount = Article.RevenueAccount,
                VatAccount = Article.VatAccount,
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

            Article = await database.Articles
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-article-accounting");
        }
        catch (MissingRevenueAccountException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.RevenueAccount),
                "Ange intäktskonto.");

            return Page();
        }
        catch (MissingVatAccountException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.VatAccount),
                "Ange moms-konto.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
