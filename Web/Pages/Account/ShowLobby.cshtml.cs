namespace Betalish.Web.Pages.Account;

public class ShowLobbyModel(
    IUserToken userToken,
    IDatabaseService database) : UserTokenPageModel(userToken)
{
    public List<Client> Clients { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            Clients = await database.ClientAuths
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .Select(x => x.Client)
                .OrderBy(x => x.Name)
                .ToListAsync();

            IsAdmin = await database.AdminAuths
                .AnyAsync(x => x.UserId == UserToken.UserId!.Value);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
