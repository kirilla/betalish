namespace Betalish.Web.Common;

public class AdminPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;

    public void AssertIsAdmin()
    {
        if (!UserToken.IsAdmin)
            throw new NotPermittedException();
    }
}
