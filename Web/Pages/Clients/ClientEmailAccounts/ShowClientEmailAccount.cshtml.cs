namespace Betalish.Web.Pages.Clients.ClientEmailAccounts;

public class ShowClientEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public ClientEmailAccount ClientEmailAccount { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsClient();

            ClientEmailAccount = await database.EmailAccounts
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
