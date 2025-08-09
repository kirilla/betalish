namespace Betalish.Web.Pages.Clients.EmailAccounts;

public class ShowEmailAccountModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public EmailAccount EmailAccount { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsClient();

            EmailAccount = await database.EmailAccounts
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
