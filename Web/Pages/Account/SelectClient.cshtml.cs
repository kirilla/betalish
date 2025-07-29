using Betalish.Application.Commands.Sessions.SelectClient;

namespace Betalish.Web.Pages.Account;

public class SelectClientModel(
    IUserToken userToken,
    IDatabaseService database,
    ISelectClientCommand command) : UserTokenPageModel(userToken)
{
    public Client Client { get; set; } = null!;

    [BindProperty]
    public SelectClientCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            var auth = await database.ClientAuths
                .Where(x =>
                    x.ClientId == id &&
                    x.UserId == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x => 
                    x.Id == id &&
                    x.ClientAuths.Any(y => 
                        y.UserId == UserToken.UserId!.Value))
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new SelectClientCommandModel()
            {
                Id = id,
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
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            var auth = await database.ClientAuths
                .Where(x =>
                    x.ClientId == CommandModel.Id &&
                    x.UserId == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotPermittedException();

            Client = await database.Clients
                .Where(x =>
                    x.Id == CommandModel.Id &&
                    x.ClientAuths.Any(y =>
                        y.UserId == UserToken.UserId!.Value))
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-client-desktop");
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
