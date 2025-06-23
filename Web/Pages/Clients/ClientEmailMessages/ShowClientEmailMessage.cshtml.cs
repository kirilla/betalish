namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class ShowClientEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public ClientEmailMessage ClientEmailMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertClientAuthorization(database);

            Client = await database.Clients
                .Where(x => x.Id == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            ClientEmailMessage = await database.ClientEmailMessages
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
