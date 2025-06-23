using Betalish.Application.Commands.Sessions.EndSession;

namespace Betalish.Web.Pages.Account;

public class EndSessionModel(
    IUserToken userToken,
    IDatabaseService database,
    IEndSessionCommand command) : UserTokenPageModel(userToken)
{
    public Session Session { get; set; }

    [BindProperty]
    public EndSessionCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Session = await database.Sessions
                .Where(x =>
                    x.Id == id &&
                    x.UserId == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EndSessionCommandModel()
            {
                Id = Session.Id,
            };

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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Session = await database.Sessions
                .Where(x =>
                    x.Id == CommandModel.Id &&
                    x.UserId == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/sessions");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
