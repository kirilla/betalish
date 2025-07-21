using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.OnboardingSteps.EditOnboardingStep;

public class EditOnboardingStepCommandModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange steg.")]
    public OnboardingStepKind? OnboardingStepKind { get; set; }

    public bool Done { get; set; }
}
