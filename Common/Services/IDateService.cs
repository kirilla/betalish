namespace Betalish.Common.Services;

public interface IDateService
{
    DateOnly GetDateOnlyToday();
    DateTime GetDateTimeNow();
}
