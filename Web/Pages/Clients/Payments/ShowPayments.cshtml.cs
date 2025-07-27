namespace Betalish.Web.Pages.Clients.Payments;

public class ShowPaymentsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<Payment> Payments { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Payments = await database.Payments
                .AsNoTracking()
                .Include(x => x.PaymentAccount)
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Date)
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
