namespace Betalish.Web.Pages.Clients.OnboardingSteps;

public class ShowOnboardingStepModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public OnboardingStep OnboardingStep { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            OnboardingStep = await database.OnboardingSteps
                .Where(x => 
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

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
