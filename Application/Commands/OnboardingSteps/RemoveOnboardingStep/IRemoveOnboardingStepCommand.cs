namespace Betalish.Application.Commands.OnboardingSteps.RemoveOnboardingStep;

public interface IRemoveOnboardingStepCommand
{
    Task Execute(IUserToken userToken, RemoveOnboardingStepCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
