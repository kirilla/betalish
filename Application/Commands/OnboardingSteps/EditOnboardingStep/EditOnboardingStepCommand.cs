namespace Betalish.Application.Commands.OnboardingSteps.EditOnboardingStep;

public class EditOnboardingStepCommand(IDatabaseService database) : IEditOnboardingStepCommand
{
    public async Task Execute(
        IUserToken userToken, EditOnboardingStepCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.OnboardingSteps
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.OnboardingStepKind == model.OnboardingStepKind!.Value &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        var step = await database.OnboardingSteps
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        step.OnboardingStepKind = model.OnboardingStepKind!.Value;
        step.Done = model.Done;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
