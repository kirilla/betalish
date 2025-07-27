namespace Betalish.Common.Services;

public interface IDateService
{
    DateOnly GetDateOnlyNow();
    DateTime GetDateTimeNow();
}
