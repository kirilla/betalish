namespace Betalish.Web.Pages.Admin.Users;

public class ShowUsersModel(
    IDatabaseService database,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public int UserCount { get; set; }
    public int AdminUserCount { get; set; }
    public int ClientUserCount { get; set; }
    public int OtherUserCount { get; set; }

    public int SessionCount { get; set; }

    public int BadSignInCount { get; set; }

    public int SignupCount { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            UserCount = await database.Users.CountAsync();

            AdminUserCount = await database.Users
                .CountAsync(x => x.AdminAuths.Any());
            
            ClientUserCount = await database.Users
                .CountAsync(x => x.ClientAuths.Any());

            OtherUserCount = await database.Users
                .CountAsync(x => 
                    !x.AdminAuths.Any() &&
                    !x.ClientAuths.Any());

            SessionCount = await database.Sessions.CountAsync();
            BadSignInCount = await database.BadSignIns.CountAsync();
            SignupCount = await database.Signups.CountAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
