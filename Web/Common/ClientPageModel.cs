namespace Betalish.Web.Common;

public class ClientPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;

    public void AssertIsClient()
    {
        if (!UserToken.IsClient)
            throw new NotPermittedException();
    }
}
