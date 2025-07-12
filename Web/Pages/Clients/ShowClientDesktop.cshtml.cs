namespace Betalish.Web.Pages.Clients;

public class ShowClientDesktopModel(IUserToken userToken) : ClientPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

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
