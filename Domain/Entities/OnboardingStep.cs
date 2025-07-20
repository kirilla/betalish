namespace Betalish.Domain.Entities;

public class OnboardingStep : IValidateOnSave
{
    public int Id { get; set; }

    public OnboardingStepKind OnboardingStepKind { get; set; }

    public bool Done { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public void ValidateOnSave()
    {
        if (!Enum.IsDefined(OnboardingStepKind))
            throw new InvalidEnumException();
    }
}
