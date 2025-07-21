using Betalish.Application.Commands.OnboardingSteps.EditOnboardingStep;

namespace Betalish.Web.Pages.Clients.OnboardingSteps;

public class EditOnboardingStepModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditOnboardingStepCommand command) : ClientPageModel(userToken)
{
    public OnboardingStep OnboardingStep { get; set; } = null!;

    [BindProperty]
    public EditOnboardingStepCommandModel CommandModel { get; set; }
        = new EditOnboardingStepCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            OnboardingStep = await database.OnboardingSteps
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditOnboardingStepCommandModel()
            {
                Id = OnboardingStep.Id,
                OnboardingStepKind = OnboardingStep.OnboardingStepKind,
                Done = OnboardingStep.Done,
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            OnboardingStep = await database.OnboardingSteps
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
