using Betalish.Application.Commands.Tests.TestLogItemDedup;

namespace Betalish.Web.Pages.Admin.Tests;

public class TestLogItemDedupModel(
    IDatabaseService database,
    ITestLogItemDedupCommand command,
    IUserToken userToken) : AdminPageModel(userToken)
{
    [BindProperty]
    public TestLogItemDedupCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
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
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
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
                "Bekräfta att du verkligen vill ta bort användaren.");

            return Page();
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
