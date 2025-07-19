namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomerTagModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public CustomerTag CustomerTag { get; set; } = null!;

    public List<CustomerTag> CustomerTags { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            CustomerTag = await database.CustomerTags
                .Where(x =>
                    x.Customer.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CustomerTags = await database.CustomerTags
                .AsNoTracking()
                .Include(x => x.Customer)
                .Where(x =>
                    x.Customer.ClientId == UserToken.ClientId!.Value &&
                    x.Key == CustomerTag.Key)
                .ToListAsync();

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
