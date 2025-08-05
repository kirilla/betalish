using System.ComponentModel.DataAnnotations;

namespace Betalish.Common.Attributes;

public class RequiredIfBooleanAttribute(
    string dependentProperty, 
    bool expectedValue, 
    string errorMessage) : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(dependentProperty);

        if (property == null)
            return new ValidationResult($"Unknown property: {dependentProperty}");

        var dependentValue = property.GetValue(validationContext.ObjectInstance);

        if (dependentValue is bool actualValue && actualValue == expectedValue)
        {
            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
            {
                return new ValidationResult(errorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
