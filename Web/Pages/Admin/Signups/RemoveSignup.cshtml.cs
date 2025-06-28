using Betalish.Application.Commands.Signups.RemoveSignup;

namespace Betalish.Web.Pages.Admin.Signups;

public class RemoveSignupModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveSignupCommand command) : AdminPageModel(userToken)
{
    public Signup Signup { get; set; }

    [BindProperty]
    public RemoveSignupCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            Signup = await database.Signups
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveSignupCommandModel()
            {
                Id = Signup.Id,
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

            Signup = await database.Signups
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-signups");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort ansökan.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
