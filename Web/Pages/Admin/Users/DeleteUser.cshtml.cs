using Betalish.Application.Commands.Users.DeleteUser;

namespace Betalish.Web.Pages.Admin.Users;

public class DeleteUserModel(
    IDatabaseService database,
    IDeleteUserCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    public new User User { get; set; }

    [BindProperty]
    public DeleteUserCommandModel CommandModel { get; set; }

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

            CommandModel = new DeleteUserCommandModel()
            {
                Id = User.Id,
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
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/admin/show-users");
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
