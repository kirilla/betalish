using Betalish.Application.Commands.ClientAuths.GrantClientAuth;

namespace Betalish.Web.Pages.Admin.ClientAuths;

public class GrantClientAuthModel(
    IDatabaseService database,
    IGrantClientAuthCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public new User User { get; set; } = null!;

    public List<Client> Clients { get; set; } = [];

    [BindProperty]
    public GrantClientAuthCommandModel CommandModel { get; set; } = new();

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

            Clients = await database.Clients
                .AsNoTracking()
                .Where(x => !x.ClientAuths.Any(y => y.UserId == userId))
                .OrderBy(x => x.Name)
                .ToListAsync();

            CommandModel = new GrantClientAuthCommandModel()
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
                .Where(x => x.Id == CommandModel.UserId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Clients = await database.Clients
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-user/{userId}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.ClientId),
                "Användaren är redan admin för organisationen.");

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
