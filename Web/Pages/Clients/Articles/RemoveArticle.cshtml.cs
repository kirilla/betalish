using Betalish.Application.Commands.Articles.RemoveArticle;

namespace Betalish.Web.Pages.Clients.Articles;

public class RemoveArticleModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveArticleCommand command) : ClientPageModel(userToken)
{
    public Article Article { get; set; } = null!;

    [BindProperty]
    public RemoveArticleCommandModel CommandModel { get; set; }
        = new RemoveArticleCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Article = await database.Articles
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveArticleCommandModel()
            {
                Id = Article.Id,
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
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-articles");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
