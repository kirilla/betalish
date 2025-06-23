namespace Betalish.Web.Pages.Clients;

public class ShowClientDesktopModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database);

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch (NotPermittedException)
        {
            return Redirect("/help/notpermitted");
        }
        catch
        {
            return Redirect("/error");
        }
    }
}
