namespace Betalish.Common.Constants;

public static class Defaults
{
    public static class Accounting
    {
        public const string AccountsReceivable = "1510";
        public const string Rounding = "3740";
    }

    public static class Invoice
    {
        public static class PaymentTermDays
        {
            public const int Min = 8;
            public const int Max = 120;
            public const int Default = 30;
        }

        public const decimal MinToConsiderPaid = 20m;
    }
}
