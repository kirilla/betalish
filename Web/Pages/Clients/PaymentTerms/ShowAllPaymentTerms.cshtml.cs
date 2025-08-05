namespace Betalish.Web.Pages.Clients.PaymentTerms;

public class ShowAllPaymentTermsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<Domain.Entities.PaymentTerms> PaymentTerms { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            PaymentTerms = await database.PaymentTerms
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Name)
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
