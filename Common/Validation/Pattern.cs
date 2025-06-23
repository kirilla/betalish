namespace Betalish.Common.Validation;

public static class Pattern
{
    public static class Common
    {
        public const string Anything = @"^.*$";
        public const string AnythingMultiLine = @"^(\n|\r|.)*$";
        public const string SomeContent = @"^.*[\S].*$";

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
    }
}
