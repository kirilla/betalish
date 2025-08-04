using Betalish.Application.Commands.BillingStrategies.AddBillingStrategy;

namespace Betalish.Web.Pages.Clients.BillingStrategies;

public class AddBillingStrategyModel(
    IUserToken userToken,
    IAddBillingStrategyCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddBillingStrategyCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddBillingStrategyCommandModel();

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

            return Redirect($"/show-billing-strategy/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns redan en strategy med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
