using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.OnboardingSteps.AddOnboardingStep;

public class AddOnboardingStepCommandModel
{
    [Required(ErrorMessage = "Ange steg.")]
    public OnboardingStepKind? OnboardingStepKind { get; set; }

    public bool Done { get; set; }
}
