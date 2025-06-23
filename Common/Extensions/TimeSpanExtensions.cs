namespace Betalish.Common.Extensions;

public static class TimeSpanExtensions
{
    public static string? ToSummary(this TimeSpan span)
    {
        var days = span.Days;
        var hours = span.Hours;
        var minutes = span.Minutes;

        if (span.TotalMinutes < 3)
            return null;

        List<string> strings = new List<string>();

        if (days > 0)
            strings.Add($"{days} dagar");

        if (hours > 0)
            strings.Add($"{hours} timmar");

        if (minutes > 0)
            strings.Add($"{minutes} minuter");

        return strings.Join(", ");
    }
}
