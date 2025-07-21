using Betalish.Common.Locale;

namespace Betalish.Common.Extensions;

public static class NumberExtensions
{
    public static string ToSwedish(this int number)
    {
        return number.ToString("N0", Swedish.CultureInfo);
    }
}
