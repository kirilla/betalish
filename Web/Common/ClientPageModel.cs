namespace Betalish.Web.Common;

public class ClientPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;

    public Client Client { get; set; }

    public ClientNavbarViewModel ClientNavbarViewModel => 
        new ClientNavbarViewModel(UserToken, Client);

    // UserToken comes from dependency injection,
    // via the constructor.

    // Client is populated by a database lookup
    // in the page model subclass.

    // The ClientNavbarViewModel getter is a convenience,
    // meant to be passed as an argument for the
    // _ClientNavbar partial view.

    public async Task AssertClientAuthorization(
        IDatabaseService database, int clientId)
    {
        if (!await database.ClientAuths.AnyAsync(x =>
                x.ClientId == clientId &&
                x.UserId == UserToken.UserId!.Value))
            throw new NotPermittedException();
    }
}
