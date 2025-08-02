using Betalish.Application.Commands.BillingPlans.AddBillingPlan;

namespace Betalish.Web.Pages.Clients.BillingPlans;

public class AddBillingPlanModel(
    IUserToken userToken,
    IAddBillingPlanCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddBillingPlanCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddBillingPlanCommandModel();

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

            return Redirect("/show-billing-plans");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns redan en plan med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
