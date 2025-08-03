using Betalish.Application.Commands.BillingPlanItems.AddBillingPlanItem;

namespace Betalish.Web.Pages.Clients.BillingPlanItems;

public class AddBillingPlanItemModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddBillingPlanItemCommand command) : ClientPageModel(userToken)
{
    public BillingPlan BillingPlan { get; set; } = null!;

    [BindProperty]
    public AddBillingPlanItemCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new AddBillingPlanItemCommandModel()
            {
                BillingPlanId = id,
            };

            return Page();
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
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-billing-plan/{id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
