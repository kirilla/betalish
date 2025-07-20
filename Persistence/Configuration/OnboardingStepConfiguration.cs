namespace Betalish.Persistence.Configuration;

class OnboardingStepConfiguration : IEntityTypeConfiguration<OnboardingStep>
{
    public void Configure(EntityTypeBuilder<OnboardingStep> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
