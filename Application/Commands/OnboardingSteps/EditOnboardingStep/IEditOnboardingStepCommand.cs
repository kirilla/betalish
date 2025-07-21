namespace Betalish.Application.Commands.OnboardingSteps.EditOnboardingStep;

public interface IEditOnboardingStepCommand
{
    Task Execute(IUserToken userToken, EditOnboardingStepCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
