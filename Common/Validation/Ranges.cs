namespace Betalish.Common.Validation;

public static class Ranges
{
    public static class Invoice
    {
        public static class Number
        {
            public const int Min = 1;
            public const int Max = 99999999; // 8 digits;
        }
    }

    public static class Smtp
    {
        public static class Port
        {
            public const int Min = 1;
            public const int Max = UInt16.MaxValue;
        }
    }
}
