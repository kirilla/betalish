using Betalish.Common.Extensions;
using Betalish.Common.Validation;

namespace Betalish.Common.Logic;

public static class OrgnumLogic
{
    private static bool IsCheckDigitValid(string orgnum)
    {
        orgnum = orgnum.Trim().StripNonNumeric();

        if (orgnum.Length != 10)
        {
            throw new ArgumentException("Unexpected orgnum length: " + orgnum.Length);
        }

        var arr = orgnum
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
                    throw new Exception($"Internt fel i {nameof(OrgnumLogic)}.IsCheckDigitValid");
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

    public static bool IsValidOrgnum(string orgnum)
    {
        if (string.IsNullOrWhiteSpace(orgnum))
            return false;

        if (orgnum.Length != 10)
            return false;

        if (!RegexLogic.IsMatch(orgnum, Pattern.Common.Organization.Orgnum))
            return false;

        return IsCheckDigitValid(orgnum);
    }
}
