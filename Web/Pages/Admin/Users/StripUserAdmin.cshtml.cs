using Betalish.Application.Commands.Users.StripUserAdmin;

namespace Betalish.Web.Pages.Admin.Users;

public class StripUserAdminModel(
    IDatabaseService database,
    IStripUserAdminCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    [BindProperty]
    public StripUserAdminCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            User = await database.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new StripUserAdminCommandModel()
            {
                UserId = User.Id,
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
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            User = await database.Users
                .Where(x => x.Id == CommandModel.UserId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/admin/show-user/{User.Id}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort användaren.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
