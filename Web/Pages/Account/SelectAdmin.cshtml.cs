using Betalish.Application.Commands.Sessions.SelectAdmin;
using Betalish.Application.Queues.LogItems;

namespace Betalish.Web.Pages.Account;

public class SelectAdminModel(
    IUserToken userToken,
    IDatabaseService database,
    ILogItemList logItemList,
    ISelectAdminCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public SelectAdminCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new SelectAdminCommandModel();

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
                LogItemKind = LogItemKind.SelectAdmin,
                UserId = UserToken.UserId!.Value,
            });

            return Redirect($"/show-admin-desktop");
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
