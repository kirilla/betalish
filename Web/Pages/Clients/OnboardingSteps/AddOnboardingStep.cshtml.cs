using Betalish.Application.Commands.OnboardingSteps.AddOnboardingStep;

namespace Betalish.Web.Pages.Clients.OnboardingSteps;

public class AddOnboardingStepModel(
    IUserToken userToken,
    IAddOnboardingStepCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddOnboardingStepCommandModel CommandModel { get; set; }
        = new AddOnboardingStepCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddOnboardingStepCommandModel();

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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-onboarding-steps");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.OnboardingStepKind),
                "Steget finns redan.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
