namespace Betalish.Common.Logic;

public static class AccountingLogic
{
    public static (decimal debit, decimal credit) Normalize(decimal debit, decimal credit)
    {
        decimal net = credit - debit;

        if (net > 0)
        {
            debit = 0;
            credit = net;
        }
        else if (net < 0)
        {
            debit = -net;
            credit = 0;
        }
        else
        {
            debit = 0;
            credit = 0;
        }

        return (debit, credit);
    }
}
