namespace Betalish.Common.Services;

public class DateService : IDateService
{
    public DateTime GetDateTimeNow()
    {
        return DateTime.Now;
    }

    public DateOnly GetDateOnlyNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}
