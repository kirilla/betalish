namespace Betalish.Common.Validation;

public static class Limits
{
    public static class User
    {
        public static class EmailAddresses
        {
            public const int Max = 3;
        }

        public static class PhoneNumbers
        {
            public const int Max = 3;
        }
    }
}
