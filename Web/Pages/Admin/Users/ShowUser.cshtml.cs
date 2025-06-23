using Betalish.Application.Commands.ClientAuths.GrantClientAuth;
using Betalish.Application.Commands.ClientAuths.RevokeClientAuth;
using Betalish.Application.Commands.Users.MakeUserAdmin;
using Betalish.Application.Commands.Users.StripUserAdmin;

namespace Betalish.Web.Pages.Admin.Users;

public class ShowUserModel(
    IUserToken userToken,
    IDatabaseService database,
    IMakeUserAdminCommand makeUserAdminCommand,
    IStripUserAdminCommand stripUserAdminCommand,
    IGrantClientAuthCommand grantClientAuthCommand,
    IRevokeClientAuthCommand revokeClientAuthCommand) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    public List<Client> Clients { get; set; }
    public List<UserEmail> UserEmails { get; set; }

    public bool CanMakeUserAdmin { get; set; }
    public bool CanStripUserAdmin { get; set; }
    public bool CanGrantClientAuth { get; set; }
    public bool CanRevokeClientAuth { get; set; }

    public bool IsAdmin { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            User = await database.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            UserEmails = await database.UserEmails
                .Where(x => x.UserId == id)
                .ToListAsync();

            Clients = await database.ClientAuths
                .Where(x => x.UserId == id)
                .Select(x => x.Client)
                .OrderBy(x => x.Name)
                .ToListAsync();

            CanMakeUserAdmin = await makeUserAdminCommand.IsPermitted(userToken);
            CanStripUserAdmin = await stripUserAdminCommand.IsPermitted(userToken);
            CanGrantClientAuth = await grantClientAuthCommand.IsPermitted(userToken);
            CanRevokeClientAuth = await revokeClientAuthCommand.IsPermitted(userToken);

            IsAdmin = await database.AdminAuths.AnyAsync(x => x.UserId == id);

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
