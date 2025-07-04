using Betalish.Application.Commands.Sessions.UnselectClient;
using Betalish.Application.Queues.LogItems;

namespace Betalish.Web.Pages.Account;

public class UnselectClientModel(
    IUserToken userToken,
    IDatabaseService database,
    ILogItemList logItemList,
    IUnselectClientCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public UnselectClientCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new UnselectClientCommandModel();

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            logItemList.AddLogItem(new LogItem()
            {
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LogItemKind = LogItemKind.UnselectClient,
                UserId = UserToken.UserId!.Value,
            });

            return Redirect($"/show-lobby");
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
