using Betalish.Common.Locale;

namespace Betalish.Common.Extensions;

public static class DecimalExtensions
{
    public static decimal? TryParseDecimal(this string s)
    {
        return decimal.TryParse(s, Swedish.CultureInfo, out var num) ? num : (decimal?)null;
    }

    public static decimal RoundToEven(this decimal d)
    {
        return Math.Round(d, 2, MidpointRounding.ToEven);
    }

    public static string ToSwedish(this decimal value)
    {
        return value.ToString("N2", Swedish.CultureInfo);
    }
}
