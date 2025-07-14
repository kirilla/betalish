namespace Betalish.Common.Validation;

public static class Pattern
{
    public static class Common
    {
        public const string Anything = @"^.*$";
        public const string AnythingMultiLine = @"^(\n|\r|.)*$";
        public const string SomeContent = @"^.*[\S].*$";

        public static class Bookkeeping
        {
            public const string Account = @"^\d{4}$";
        }

        public static class Decimal
        {
            public static class Signed
            {
                public const string TwoOptionalDecimals = @"^-?[\d\s]*\d(,\d{0,2})?$";
                public const string FourOptionalDecimals = @"^-?[\d\s]*\d(,\d{0,4})?$";
                public const string SixOptionalDecimals = @"^-?[\d\s]*\d(,\d{0,6})?$";
            }

            public static class Unsigned
            {
                public const string TwoOptionalDecimals = @"^[\d\s]*\d(,\d{0,2})?$";
                public const string FourOptionalDecimals = @"^[\d\s]*\d(,\d{0,4})?$";
                public const string SixOptionalDecimals = @"^[\d\s]*\d(,\d{0,6})?$";
            }
        }

        public static class Email
        {
            public const string Address =
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        }

        public static class Mime
        {
            public const string Type =
                @"^[a-zA-Z0-9!#$&^_.+-]+/[a-zA-Z0-9!#$&^_.+-]+$";
        }

        public static class Organization
        {
            public const string Orgnum = @"^\d{10}$";
            public const string OrgnumPermissive = @"\d{6}.?\d{4}"; // open-ended
        }

        public static class Percentage
        {
            public static class Signed
            {
                public const string Percent = @"^-?\d+(,\d+)?$";
            }

            public static class Unsigned
            {
                public const string Percent = @"^\d+(,\d+)?$";
            }
        }

        public static class Phone
        {
            public const string Number = @"^[-\d\s+\(\)]+$";
        }

        public static class Ssn
        {
            public const string Ssn10 = @"^\d{10}$";
            public const string Ssn12 = @"^\d{12}$";

            public const string Ssn10Permissive = @"\d{6}.?\d{4}"; // open-ended
            public const string Ssn12Permissive = @"\d{8}.?\d{4}"; // open-ended

            public const string Ssn12Century = @"^(19|20)\d{10}$";

            public const string Samordningsnummer10 = @"^\d{4}[6-9]\d{5}$";
            public const string Samordningsnummer12 = @"^\d{6}[6-9]\d{5}$";

            public const string Ssn10Female = @"^\d{8}[02468]\d$";
            public const string Ssn10Male = @"^\d{8}[13579]\d$";
        }
    }
}
