namespace Betalish.Web.Pages.Clients.PaymentTerms;

public class ShowPaymentTermsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Domain.Entities.PaymentTerms PaymentTerms { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            PaymentTerms = await database.PaymentTerms
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
