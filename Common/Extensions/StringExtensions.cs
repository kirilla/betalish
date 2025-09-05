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

    public static bool HasValue(this string? s)
    {
        return !string.IsNullOrWhiteSpace(s);
    }

    public static bool IsMissingValue(this string? s)
    {
        return string.IsNullOrWhiteSpace(s);
    }
}
