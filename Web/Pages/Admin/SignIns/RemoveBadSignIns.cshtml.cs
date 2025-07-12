using Betalish.Application.Commands.BadSignIns.RemoveBadSignIns;

namespace Betalish.Web.Pages.Admin.SignIns;

public class RemoveBadSignInsModel(
    IUserToken userToken,
    IRemoveBadSignInsCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public RemoveBadSignInsCommandModel CommandModel { get; set; }
        = new RemoveBadSignInsCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!UserToken.IsAdmin)
                throw new NotPermittedException();

            CommandModel = new RemoveBadSignInsCommandModel();

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
            AssertIsAdmin();

            if (!userToken.IsAdmin)
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-bad-signins");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
