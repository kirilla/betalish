using Betalish.Application.Commands.BillingPlans.EditBillingPlan;

namespace Betalish.Web.Pages.Clients.BillingPlans;

public class EditBillingPlanModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditBillingPlanCommand command) : ClientPageModel(userToken)
{
    public BillingPlan BillingPlan { get; set; } = null!;

    [BindProperty]
    public EditBillingPlanCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditBillingPlanCommandModel()
            {
                Id = BillingPlan.Id,
                Name = BillingPlan.Name,
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

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-billing-plans");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annan tidsplan med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
