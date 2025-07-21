namespace Betalish.Common.Logic;

public static class MathLogic
{
    public static (decimal rounded, decimal rounding) Round(decimal amount)
    {
        if (amount == 0) return (0, 0);

        decimal rounded = Math.Round(amount, 0, MidpointRounding.ToEven);
        decimal rounding = Math.Round(amount, 2) - rounded;

        return (rounded, -rounding);
    }
}
