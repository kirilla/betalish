using Betalish.Common.Logic;

namespace Betalish.Common.Extensions;

public static class AccountingExtensions
{
    public static bool AccountIsValid(this string? account)
    {
        return
            account != null &&
            account.Length >= MinLengths.Common.Bookkeeping.Account &&
            account.Length <= MaxLengths.Common.Bookkeeping.Account &&
            RegexLogic.IsMatch(account, Pattern.Common.Bookkeeping.Account);
    }
}
