namespace Betalish.Web.Pages.Admin.LogItems;

public class ShowLogItemsModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public List<LogItem> LogItems { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            LogItems = await database.LogItems
                .AsNoTracking()
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
