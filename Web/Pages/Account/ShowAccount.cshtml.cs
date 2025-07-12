namespace Betalish.Web.Pages.Account;

public class ShowAccountModel(
    IDatabaseService database,
    IUserToken userToken) : UserTokenPageModel(userToken)
{
    public new User User { get; set; }

    public List<UserEmail> UserEmails { get; set; }
    public List<UserPhone> UserPhones { get; set; }
    public List<UserSsn> UserSsns { get; set; }

    public bool CanAddUserEmail { get; set; }
    public bool CanRemoveUserEmail { get; set; }
    public bool CanAddAccountPhone { get; set; }
    public bool CanRemoveAccountPhone { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            UserEmails = await database.UserEmails
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            UserPhones = await database.UserPhones
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            UserSsns = await database.UserSsns
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            CanAddUserEmail = UserEmails.Count < Limits.User.EmailAddresses.Max;
            CanAddAccountPhone = UserPhones.Count < Limits.User.PhoneNumbers.Max;

            CanRemoveUserEmail = UserEmails.Count > 1;
            CanRemoveAccountPhone = UserPhones.Count > 1;

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
