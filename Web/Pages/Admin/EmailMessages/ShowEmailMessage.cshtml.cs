namespace Betalish.Web.Pages.Admin.EmailMessages;

public class ShowEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database) : AdminPageModel(userToken)
{
    public EmailMessage EmailMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            EmailMessage = await database.EmailMessages
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
