namespace Betalish.Web.Pages.Admin.NetworkRules;

public class ShowNetworkRuleModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public NetworkRule NetworkRule { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            NetworkRule = await database.NetworkRules
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
