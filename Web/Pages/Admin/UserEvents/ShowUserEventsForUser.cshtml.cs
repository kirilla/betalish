namespace Betalish.Web.Pages.Admin.UserEvents;

public class ShowUserEventsForUserModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    public List<UserEvent> UserEvents { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ?? 
                throw new NotFoundException();

            UserEvents = await database.UserEvents
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
