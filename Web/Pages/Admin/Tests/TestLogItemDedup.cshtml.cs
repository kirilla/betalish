using Betalish.Application.Commands.Tests.TestLogItemDedup;

namespace Betalish.Web.Pages.Admin.Tests;

public class TestLogItemDedupModel(
    ITestLogItemDedupCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    [BindProperty]
    public TestLogItemDedupCommandModel CommandModel { get; set; }
        = new TestLogItemDedupCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new TestLogItemDedupCommandModel();

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

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-tests");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du vill genomföra testet.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
