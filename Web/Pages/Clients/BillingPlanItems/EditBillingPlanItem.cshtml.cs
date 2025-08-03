using Betalish.Application.Commands.BillingPlanItems.EditBillingPlanItem;

namespace Betalish.Web.Pages.Clients.BillingPlanItems;

public class EditBillingPlanItemModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditBillingPlanItemCommand command) : ClientPageModel(userToken)
{
    public BillingPlan BillingPlan { get; set; } = null!;
    public BillingPlanItem BillingPlanItem { get; set; } = null!;

    [BindProperty]
    public EditBillingPlanItemCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BillingPlanItem = await database.BillingPlanItems
                .Where(x =>
                    x.Id == id &&
                    x.BillingPlan.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.Id == BillingPlanItem.BillingPlanId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditBillingPlanItemCommandModel()
            {
                Id = BillingPlanItem.Id,
                OnDay = BillingPlanItem.OnDay,
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

            BillingPlanItem = await database.BillingPlanItems
                .Where(x =>
                    x.Id == id &&
                    x.BillingPlan.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.Id == BillingPlanItem.BillingPlanId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();
            
            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-billing-plan/{BillingPlan.Id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
