using Betalish.Common.Exceptions;
using Betalish.Common.Logic;

namespace Betalish.Common.Extensions;

public static class OrgnumExtensions
{
    public static void AssertOrgnumValid(this string orgnum)
    {
        if (string.IsNullOrWhiteSpace(orgnum))
            throw new InvalidOrgnumException();

        if (!OrgnumLogic.IsValidOrgnum(orgnum))
            throw new InvalidOrgnumException();
    }

    public static string WithDash(this string orgnum)
    {
        if (orgnum.Length == 10)
            return orgnum.Insert(6, "-");

        return orgnum;
    }
}
