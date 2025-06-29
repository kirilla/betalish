namespace Betalish.Web.Common;

public class ClientPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;

    public Client Client { get; set; }

    public async Task AssertClientAuthorization(
        IDatabaseService database)
    {
        if (!await database.ClientAuths.AnyAsync(x =>
                x.ClientId == UserToken.ClientId!.Value &&
                x.UserId == UserToken.UserId!.Value))
            throw new NotPermittedException();
    }
}
