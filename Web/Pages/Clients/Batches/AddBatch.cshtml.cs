using Betalish.Application.Commands.Batches.AddBatch;

namespace Betalish.Web.Pages.Clients.Batches;

public class AddBatchModel(
    IUserToken userToken,
    IAddBatchCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddBatchCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddBatchCommandModel();

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

            return Redirect("/show-batches");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns redan en batch med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
