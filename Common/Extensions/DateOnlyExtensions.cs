namespace Betalish.Common.Extensions;

public static class DateOnlyExtensions
{
    public static string? ToIso8601(this DateOnly date)
    {
        return date.ToString("O", CultureInfo.InvariantCulture);
    }

    public static string? ToIso8601(this DateOnly? date)
    {
        return date?.ToIso8601();
    }
}
