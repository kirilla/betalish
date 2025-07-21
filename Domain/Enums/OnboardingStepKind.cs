namespace Betalish.Domain.Enums;

public enum OnboardingStepKind
{
    InvoiceRange = 251447550,
}

public static class OnboardingStepKindExtensions
{
    public static int ToOrdinal(this OnboardingStepKind kind)
    {
        return kind switch
        {
            OnboardingStepKind.InvoiceRange => 1,
            _ => 0,
        };
    }
}
