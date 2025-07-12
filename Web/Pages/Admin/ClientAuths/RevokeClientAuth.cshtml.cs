using Betalish.Application.Commands.ClientAuths.RevokeClientAuth;

namespace Betalish.Web.Pages.Admin.ClientAuths;

public class RevokeClientAuthModel(
    IDatabaseService database,
    IRevokeClientAuthCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    public List<Client> Clients { get; set; }

    [BindProperty]
    public RevokeClientAuthCommandModel CommandModel { get; set; } = new RevokeClientAuthCommandModel();

    public async Task<IActionResult> OnGetAsync(int userId)
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == userId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Clients = await database.ClientAuths
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Client)
                .OrderBy(x => x.Name)
                .ToListAsync();

            CommandModel = new RevokeClientAuthCommandModel()
            {
                UserId = User.Id,
            };

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

    public async Task<IActionResult> OnPostAsync(int userId)
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == userId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Clients = await database.ClientAuths
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Client)
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-user/{userId}");
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
