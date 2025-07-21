namespace Betalish.Application.Commands.OnboardingSteps.AddOnboardingStep;

public class AddOnboardingStepCommand(IDatabaseService database) : IAddOnboardingStepCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddOnboardingStepCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.OnboardingSteps
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.OnboardingStepKind == model.OnboardingStepKind!.Value))
            throw new BlockedByExistingException();

        var step = new OnboardingStep()
        {
            OnboardingStepKind = model.OnboardingStepKind!.Value,
            Done = model.Done,
            ClientId = userToken.ClientId!.Value,
        };

        database.OnboardingSteps.Add(step);

        await database.SaveAsync(userToken);

        return step.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
