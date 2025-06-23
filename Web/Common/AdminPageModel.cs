namespace Betalish.Web.Common;

public class AdminPageModel(IUserToken userToken) : PageModel
{
    public IUserToken UserToken { get; } = userToken;

    public async Task AssertAdminAuthorization(IDatabaseService database)
    {
        if (!await database.AdminAuths.AnyAsync(x =>
                x.UserId == UserToken.UserId!.Value))
            throw new NotPermittedException();
    }
}
