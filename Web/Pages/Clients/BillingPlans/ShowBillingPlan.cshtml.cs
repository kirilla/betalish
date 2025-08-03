namespace Betalish.Web.Pages.Clients.BillingPlans;

public class ShowBillingPlanModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public BillingPlan BillingPlan { get; set; } = null!;

    public List<BillingPlanItem> BillingPlanItems { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            BillingPlan = await database.BillingPlans
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            BillingPlanItems = await database.BillingPlanItems
                .Where(x =>
                    x.BillingPlanId == id &&
                    x.BillingPlan.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.OnDay)
                .ToListAsync();

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
}
