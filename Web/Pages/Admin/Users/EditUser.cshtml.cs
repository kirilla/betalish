using Betalish.Application.Commands.Users.EditUser;

namespace Betalish.Web.Pages.Admin.Users;

public class EditUserModel(
    IDatabaseService database,
    IEditUserCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    [BindProperty]
    public EditUserCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            var user = await database.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditUserCommandModel()
            {
                Id = user.Id,
                Name = user.Name,
            };

            return Page();
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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-user/{CommandModel.Id}");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
