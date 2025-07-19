namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomerTagsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<CustomerTag> CustomerTags { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            CustomerTags = await database.CustomerTags
                .OrderBy(x => x.Key)
                .ThenBy(x => x.Id)
                .GroupBy(e => new { e.Key })
                .Select(g => g.First())
                .ToListAsync();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
