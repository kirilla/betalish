using System.Diagnostics;

namespace Betalish.Common.Validation;

public static class MaxLengths
{
    public static class Common
    {
        // Maximum lengths are used both to define the database
        // table column widths, as well as to validate user input.

        public const int DbString = 4000;

        // A fallback max length. There's a soft limit of 4000 UTF16 chars
        // (8000 bytes) in MS SQL Server, for column type nvarchar, 
        // after which data storage fans out to additional pages.

        public const int DbSuperSize = 16_000;

        public static class Bookkeeping
        {
            public const int Account = 4;
            public const int Description = 70;
        }

        public static class Email
        {
            public const int Address = 70;
        }

        public static class Http
        {
            public const int Method = 20;
            public const int Url = 255;
            public const int UserAgent = 150;
        }

        public static class Ip
        {
            public static class Address
            {
                public const int IPv4 = 15;
                public const int IPv6 = 39;
            }

            public static class Prefix
            {
                public const int IPv4 = 18;
                public const int IPv6 = 43;
            }
        }

        public static class Mime
        {
            public const int ContentType = 255;
        }

        public static class Organization
        {
            public const int Orgnum = 10;
            public const int OrgnumPermissive = 11;
        }

        public static class Password
        {
            public const int Clear = 70;
            public const int Hash = 128;
        }

        public static class Percent
        {
            public const int ShortWithDecimals = 7;
        }

        public static class Person
        {
            public const int FullName = 50;
        }

        public static class Phone
        {
            public const int Number = 20;
        }

        public static class Postal
        {
            public const int Address = 50;
            public const int ZipCode = 10;
            public const int City = 25;
        }

        public static class Ssn
        {
            public const int Ssn10 = 10;
            public const int Ssn12 = 12;

            public const int Ssn10Permissive = 11;
            public const int Ssn12Permissive = 13;
        }
    }

    public static class Domain
    {
        public static class Article
        {
            public const int Name = 50;
            public const int UnitName = 20;
        }

        public static class BadSignIn
        {
            public const int Name = 200;
            public const int Password = 200;
        }

        public static class Client
        {
            public const int Name = Common.Person.FullName;
            public const int Address = Common.Email.Address;
        }

        public static class ClientEmailAccount
        {
            public const int FromName = Common.Person.FullName;
            public const int FromAddress = Common.Email.Address;
            public const int ReplyToName = Common.Person.FullName;
            public const int ReplyToAddress = Common.Email.Address;
            public const int Password = 100;
            public const int SmtpHost = 100;
        }

        public static class ClientEmailMessage
        {
            public const int ToName = Common.Person.FullName;
            public const int ToAddress = Common.Email.Address;

            public const int FromName = Common.Person.FullName;
            public const int FromAddress = Common.Email.Address;

            public const int ReplyToName = Common.Person.FullName;
            public const int ReplyToAddress = Common.Email.Address;

            public const int Subject = 300;

            public const int HtmlBody = Common.DbSuperSize;
            public const int TextBody = Common.DbSuperSize;
        }

        public static class ClientEvent
        {
            public const int Description = 200;
        }

        public static class Customer
        {
            public const int Name = Common.Person.FullName;
            public const int EmailAddress = Common.Email.Address;
        }

        public static class EmailAccount
        {
            public const int FromName = Common.Person.FullName;
            public const int FromAddress = Common.Email.Address;
            public const int ReplyToName = Common.Person.FullName;
            public const int ReplyToAddress = Common.Email.Address;
            public const int Password = 100;
            public const int SmtpHost = 100;
        }

        public static class EmailAttachment
        {
            public const int Name = 100;
            public const int ContentType = Common.Mime.ContentType;
        }

        public static class EmailImage
        {
            public const int Name = 100;
            public const int ContentType = Common.Mime.ContentType;
        }

        public static class EmailMessage
        {
            public const int ToName = Common.Person.FullName;
            public const int ToAddress = Common.Email.Address;

            public const int FromName = Common.Person.FullName;
            public const int FromAddress = Common.Email.Address;

            public const int ReplyToName = Common.Person.FullName;
            public const int ReplyToAddress = Common.Email.Address;

            public const int Subject = 300;

            public const int HtmlBody = Common.DbSuperSize;
            public const int TextBody = Common.DbSuperSize;
        }

        public static class InvoiceTemplate
        {
            public const int Name = 50;
        }

        public static class LogItem
        {
            public const int Description = 200;
            public const int Exception = 200;
            public const int InnerException = 200;
        }

        public static class NetworkRule
        {
            public const int Description = 200;
        }
        
        public static class UserEvent
        {
            public const int Description = 200;
        }
    }
}
