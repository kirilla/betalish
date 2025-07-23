using Betalish.Application.Commands.Articles.AddArticle;

namespace Betalish.Web.Pages.Clients.Articles;

public class AddArticleModel(
    IUserToken userToken,
    IAddArticleCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddArticleCommandModel CommandModel { get; set; }
        = new AddArticleCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddArticleCommandModel();

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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect("/show-articles");
        }
        catch (BlockedByNumberException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Number),
                "Det finns redan en artikel med samma nummer.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
