using Betalish.Application.Commands.Articles.EditArticle;

namespace Betalish.Web.Pages.Clients.Articles;

public class EditArticleModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditArticleCommand command) : ClientPageModel(userToken)
{
    public Article Article { get; set; } = null!;

    [BindProperty]
    public EditArticleCommandModel CommandModel { get; set; }
        = new EditArticleCommandModel();

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

            CommandModel = new EditArticleCommandModel()
            {
                Id = Article.Id,
                ArticleKind = Article.ArticleKind,
                Number = Article.Number,
                Name = Article.Name,
                UnitPrice = Article.UnitPrice.ToString("N2", Swedish.CultureInfo),
                UnitName = Article.UnitName,
                VatRate = Article.VatRate.ToString("N2", Swedish.CultureInfo),
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

            return Redirect("/show-articles");
        }
        catch (BlockedByNumberException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Number),
                "Det finns en annan artikel med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
