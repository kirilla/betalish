using Betalish.Application.Commands.OnboardingSteps.RemoveOnboardingStep;

namespace Betalish.Web.Pages.Clients.OnboardingSteps;

public class RemoveOnboardingStepModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveOnboardingStepCommand command) : ClientPageModel(userToken)
{
    public OnboardingStep OnboardingStep { get; set; } = null!;

    [BindProperty]
    public RemoveOnboardingStepCommandModel CommandModel { get; set; }
        = new RemoveOnboardingStepCommandModel();

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

            CommandModel = new RemoveOnboardingStepCommandModel()
            {
                Id = OnboardingStep.Id,
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
