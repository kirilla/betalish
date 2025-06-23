namespace Betalish.Common.Extensions;

public static class DateTimeExtensions
{
    public static string? ToFixedFormatDate(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public static string? ToFixedFormatDate(this DateTime? date)
    {
        return date?.ToFixedFormatDate();
    }

    public static string ToFixedFormatDateShortTime(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    public static string? ToFixedFormatDateShortTime(this DateTime? date)
    {
        return date?.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    public static DateTime? TryParseDate(this string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return null;

        var success = DateTime.TryParse(s, out DateTime date);

        if (success == false)
            return null;

        return date;
    }

    public static DateOnly ToDateOnly(this DateTime date)
    {
        return DateOnly.FromDateTime(date);
    }

    public static DateOnly? ToDateOnly(this DateTime? date)
    {
        if (!date.HasValue)
            return null;

        return DateOnly.FromDateTime(date.Value);
    }

    public static string ToDateSummary(this DateTime fromDate, DateTime toDate)
    {
        return
            $"{fromDate.ToDateOnly().ToIso8601()} - " +
            $"{toDate.ToDateOnly().ToIso8601()}";
    }

    public static string DateTimeSummary(this DateTime fromDate, DateTime toDate)
    {
        return
            $"{fromDate.ToFixedFormatDateShortTime()} - " +
            $"{toDate.ToFixedFormatDateShortTime()}";
    }
}
