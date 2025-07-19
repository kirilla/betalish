namespace Betalish.Common.Extensions;

public static class DecimalExtensions
{
    public static decimal? TryParseDecimal(this string s)
    {
        return decimal.TryParse(s, out var num) ? num : (decimal?)null;
    }

    public static decimal RoundToEven(this decimal d)
    {
        return Math.Round(d, 2, MidpointRounding.ToEven);
    }
}
