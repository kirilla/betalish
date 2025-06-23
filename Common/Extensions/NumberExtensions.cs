namespace Betalish.Common.Extensions;

public static class NumberExtensions
{
    public static string ToSwedishSpacing(this int number)
    {
        return number.ToString("N0", new CultureInfo("sv-SE"));
    }
}
