namespace Betalish.Application.Commands.OnboardingSteps.RemoveOnboardingStep;

public class RemoveOnboardingStepCommand(IDatabaseService database) : IRemoveOnboardingStepCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveOnboardingStepCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var step = await database.OnboardingSteps
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.OnboardingSteps.Remove(step);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
