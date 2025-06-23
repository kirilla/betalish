namespace Betalish.Web.Pages.Clients;

public class ShowClientDesktopModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public async Task<IActionResult> OnGetAsync(int clientId)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database, clientId);

            Client = await database.Clients
                .Where(x => x.Id == clientId)
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
