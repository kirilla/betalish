using System.Text.RegularExpressions;

namespace Betalish.Common.Validation;

public static class RegexService
{
    public static bool IsMatch(string input, string pattern)
    {
        return Regex.IsMatch(
            input, pattern,
            RegexOptions.None,
            TimeSpan.FromMilliseconds(300));
    }

    public static string? GetFirstCaptureGroup(string input, string pattern)
    {
        return Regex.Match(
            input, pattern,
            RegexOptions.None,
            TimeSpan.FromMilliseconds(300))
            .Groups?[1]?.Value;
    }
}
