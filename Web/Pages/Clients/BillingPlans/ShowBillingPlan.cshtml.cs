namespace Betalish.Web.Pages.Clients.BillingPlans;

public class ShowBillingPlanModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public BillingPlan BillingPlan { get; set; } = null!;

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
