using Betalish.Common.Extensions;
using Betalish.Common.Validation;

namespace Betalish.Common.Logic;

public static class SsnLogic
{
    public static bool IsCheckDigitValid(string ssn)
    {
        ssn = ssn.Trim().StripNonNumeric();

        if (ssn.Length != 10)
        {
            throw new ArgumentException("Unexpected ssn length: " + ssn.Length);
        }

        int temp;

        var arr = ssn
            .Where(c => char.IsDigit(c))
            .Reverse()
            .Select(x =>
            {
                if (int.TryParse(x.ToString(), out temp))
                {
                    return temp;
                }
                else
                {
                    throw new Exception("Internt fel i SsnLogic.IsCheckDigitValid");
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

    public static bool IsValidSsn(string ssn)
    {
        ssn = ssn
            .Trim()
            .StripNonNumeric();

        if (ssn.Length == 12)
        {
            ssn = ssn.Substring(2, 10);
        }

        if (ssn.Length != 10)
        {
            return false;
        }

        if (!RegexLogic.IsMatch(ssn, Pattern.Common.Ssn.Ssn10))
        {
            return false;
        }

        return IsCheckDigitValid(ssn);
    }
}
