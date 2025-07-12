namespace Betalish.Web.Pages.Clients.Customers;

public class AddCustomerModel(IUserToken userToken) : ClientPageModel(userToken)
{
    public IActionResult OnGet()
    {
        try
        {
            if (!UserToken.IsClient)
                throw new NotPermittedException();

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
