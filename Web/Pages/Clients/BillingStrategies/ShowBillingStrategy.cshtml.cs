namespace Betalish.Web.Pages.Clients.BillingStrategies;

public class ShowBillingStrategyModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public BillingStrategy BillingStrategy { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            BillingStrategy = await database.BillingStrategies
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
