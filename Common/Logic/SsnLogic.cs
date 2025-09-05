using Betalish.Common.Extensions;
using Betalish.Common.Validation;

namespace Betalish.Common.Logic;

public static class SsnLogic
{
    private static bool IsCheckDigitValid(string ssn)
    {
        ssn = ssn.Trim().StripNonNumeric();

        if (ssn.Length != 10)
        {
            throw new ArgumentException("Unexpected ssn length: " + ssn.Length);
        }

        var arr = ssn
            .Where(c => char.IsDigit(c))
            .Reverse()
            .Select(x =>
            {
                if (int.TryParse(x.ToString(), out int temp))
                {
                    return temp;
                }
                else
                {
                    throw new Exception($"Internt fel i {nameof(SsnLogic)}.IsCheckDigitValid");
                }
            })
            .ToArray();

        var lastDigit = arr.First();

        arr = arr.Skip(1).ToArray();

        var sum = arr
            .Select((digit, index) =>
            {
                if (index % 2 == 0)
                {
                    digit *= 2;

                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                return digit;
            })
            .Sum();

        sum += lastDigit;

        return sum % 10 == 0;
    }

    public static bool IsValidSsn10(string ssn10)
    {
        if (string.IsNullOrWhiteSpace(ssn10))
            return false;

        if (ssn10.Length != 10)
            return false;

        if (!RegexLogic.IsMatch(ssn10, Pattern.Common.Ssn.Ssn10))
            return false;

        return IsCheckDigitValid(ssn10);
    }

    public static bool IsValidSsn12(string ssn12)
    {
        if (string.IsNullOrWhiteSpace(ssn12))
            return false;

        if (ssn12.Length != 12)
            return false;

        if (!RegexLogic.IsMatch(ssn12, Pattern.Common.Ssn.Ssn12))
            return false;

        var ssn10 = ssn12.ToSsn10();

        return IsCheckDigitValid(ssn10);
    }
}
