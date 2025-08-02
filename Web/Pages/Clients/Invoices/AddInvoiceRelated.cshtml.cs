namespace Betalish.Web.Pages.Clients.Invoices;

public class AddInvoiceRelatedModel(IUserToken userToken) : ClientPageModel(userToken)
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
