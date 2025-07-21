namespace Betalish.Application.Commands.OnboardingSteps.AddOnboardingStep;

public interface IAddOnboardingStepCommand
{
    Task<int> Execute(IUserToken userToken, AddOnboardingStepCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
