namespace Betalish.Common.Constants;

public static class Email
{
    public static class Html
    {
        public const string Style =
            """
            <style>
                body {
                    background-color:#FDF5E6;
                }
            </style>
            """;

        public const string Start =
            $"""
            <!DOCTYPE html>
            <html lang="sv-SE">
            <head>
                <meta charset="utf-8">
                <meta name="viewport" content="width=device-width">
                {Style}
            </head>
            <body>
            """;

        public const string End =
            $"""
            </body>
            </html>
            """;
    }
}
