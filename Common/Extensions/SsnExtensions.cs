namespace Betalish.Common.Extensions;

public static class SsnExtensions
{
    public static DateOnly? ToDateOnly(this string ssn)
    {
        if (ssn == null)
            return null;

        if (ssn.Length == 12)
        {
            try
            {
                return DateOnly.ParseExact(
                    ssn.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        if (ssn.Length == 10)
        {
            try
            {
                return DateOnly.ParseExact(
                    ssn.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        throw new Exception(
            $"Unexpected SSN: '{ssn}' in SsnExtensions.ToDateOnly().");
    }

    public static string ToSsn10(this string ssn12)
    {
        return ssn12.Substring(2, 10);
    }

    public static string ToSsnWithDash(this string ssn)
    {
        if (ssn.Length == 10)
            return ssn.Insert(6, "-");

        if (ssn.Length == 12)
            return ssn.Insert(8, "-");

        return ssn;
    }
}
