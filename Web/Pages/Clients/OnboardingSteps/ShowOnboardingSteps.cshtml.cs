namespace Betalish.Web.Pages.Clients.OnboardingSteps;

public class ShowOnboardingStepsModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<OnboardingStep> OnboardingSteps { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            OnboardingSteps = (await database.OnboardingSteps
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync())
                .OrderBy(x => x.OnboardingStepKind.ToOrdinal())
                .ToList();

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
}
