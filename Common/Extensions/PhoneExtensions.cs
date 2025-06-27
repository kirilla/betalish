using System.Text.RegularExpressions;

namespace Betalish.Common.Extensions;

public static class PhoneExtensions
{
    public static string StripNonPhoneNumberChars(this string value)
    {
        var regex = new Regex(
            @"[^-\d\s+\(\)]",
            RegexOptions.CultureInvariant,
            TimeSpan.FromSeconds(1));

        return regex.Replace(value, "");
    }
}
