namespace Betalish.Web.Pages.Clients.Onboarding;

public class ShowOnboardingModel(IUserToken userToken) : ClientPageModel(userToken)
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
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
