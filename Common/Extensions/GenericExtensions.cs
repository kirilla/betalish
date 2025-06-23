using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Betalish.Common.Extensions;

public static class GenericExtensions
{
    public static void SetEmptyStringsToNull<T>(this T obj)
    {
        var properties = obj!
            .GetType()
            .GetProperties()
            .Where(p =>
                p.PropertyType == typeof(string) &&
                p.GetGetMethod() != null &&
                p.GetSetMethod() != null);

        foreach (var property in properties)
        {
            string? value = (string?)property.GetValue(obj, null);

            if (value != null && string.IsNullOrWhiteSpace(value))
                property.SetValue(obj, null, null);
        }
    }

    public static void SetNullStringsToEmpty<T>(this T obj)
    {
        var properties = obj!
            .GetType()
            .GetProperties()
            .Where(p =>
                p.PropertyType == typeof(string) &&
                p.GetGetMethod() != null &&
                p.GetSetMethod() != null);

        foreach (var property in properties)
        {
            string? value = (string?)property.GetValue(obj, null);

            if (value == null)
                property.SetValue(obj, string.Empty, null);
        }
    }

    public static void TrimStringProperties<T>(this T obj)
    {
        var properties = obj!
            .GetType()
            .GetProperties()
            .Where(p =>
                p.PropertyType == typeof(string) &&
                p.GetGetMethod() != null &&
                p.GetSetMethod() != null);

        foreach (var property in properties)
        {
            string? value = (string?)property.GetValue(obj, null);

            if (value != null)
                property.SetValue(obj, value.Trim(), null);
        }
    }

    public static void ToLowerInvariant<T>(this T obj)
    {
        var properties = obj!
            .GetType()
            .GetProperties()
            .Where(p =>
                p.PropertyType == typeof(string) &&
                p.GetGetMethod() != null &&
                p.GetSetMethod() != null);

        foreach (var property in properties)
        {
            string? value = (string?)property.GetValue(obj, null);

            if (value != null)
                property.SetValue(obj, value.ToLowerInvariant(), null);
        }
    }

    public static void TruncateByStringLength<T>(this T obj)
    {
        if (obj == null) 
            throw new ArgumentNullException(nameof(obj));

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(string)) 
                continue;

            var attribute = property.GetCustomAttribute<StringLengthAttribute>();
            if (attribute == null)
                continue;

            var currentValue = property.GetValue(obj) as string;
            if (currentValue == null || 
                currentValue.Length <= attribute.MaximumLength)
                continue;

            var truncatedValue = currentValue.Substring(0, attribute.MaximumLength);
            property.SetValue(obj, truncatedValue);
        }
    }

    public static void AssertEnumPropertiesAreValid(this object obj)
    {
        if (obj == null) 
            throw new ArgumentNullException(nameof(obj));

        var properties = obj
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x =>
                x.PropertyType.IsEnum ||
                (x.PropertyType.IsGenericType &&
                    x.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    x.PropertyType.GetGenericArguments()[0].IsEnum))
            .ToList();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);

            if (value == null)
                continue; // Allow null values for nullable properties

            var enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (!Enum.IsDefined(enumType, value))
            {
                throw new ArgumentOutOfRangeException(property.Name, value,
                    $"The value '{value}' is not defined in enum '{enumType.Name}'.");
            }
        }
    }
}
