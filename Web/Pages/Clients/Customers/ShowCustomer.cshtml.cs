namespace Betalish.Web.Pages.Clients.Customers;

public class ShowCustomerModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertClientAuthorization(database);

            Customer = await database.Customers
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
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
