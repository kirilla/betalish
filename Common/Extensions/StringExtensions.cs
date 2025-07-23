using System.Text.RegularExpressions;

namespace Betalish.Common.Extensions;

public static class StringExtensions
{
    public static string StripNonNumeric(this string value)
    {
        var regex = new Regex(
            @"\D",
            RegexOptions.CultureInvariant,
            TimeSpan.FromSeconds(1));

        return regex.Replace(value, "");
    }

    public static string? Truncate(this string? value, int maxLength)
    {
        if (value == null)
            return null;

        if (value.Length <= maxLength)
            return value;

        return value.Substring(0, maxLength);
    }

    public static string? TruncateWithSuffix(this string? value, int maxLength, string truncationSuffix = "…")
    {
        if (value == null)
            return null;

        if (value.Length <= maxLength)
            return value;

        return 
            value.Substring(0, maxLength - truncationSuffix.Length) +
                truncationSuffix;
    }

    public static bool HasValue(this string? s)
    {
        return !string.IsNullOrWhiteSpace(s);
    }

    public static bool IsMissingValue(this string? s)
    {
        return string.IsNullOrWhiteSpace(s);
    }
}
