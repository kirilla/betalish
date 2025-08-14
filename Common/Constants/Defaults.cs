namespace Betalish.Common.Constants;

public static class Defaults
{
    public static class Accounting
    {
        public const string AccountsReceivable = "1510";
        public const string Rounding = "3740";
    }

    public static class Fee
    {
        public static class Reminder
        {
            public const decimal Min = 0;
            public const decimal Max = 60;
            public const decimal Default = 60;
        }

        public static class Demand
        {
            public const decimal Min = 0;
            public const decimal Max = 180;
            public const decimal Default = 180;
        }

        public static class RepaymentPlan
        {
            public const decimal Min = 0;
            public const decimal Max = 170;
            public const decimal Default = 170;
        }

        public static class LatePaymentCompensation_B2B
        {
            public const decimal Min = 0;
            public const decimal Max = 450;
            public const decimal Default = 450;

            // AKA:
            // Statutory late payment fee
            // EU-direktiv 2011/7/EU

            // "Under the Late Payment Directive,
            // a fixed compensation of €40 is charged
            // for delayed payments between businesses."
        }
    }

    public static class Invoice
    {
        public static class PaymentTermDays
        {
            public const int Min = 8;
            public const int Max = 120;
            public const int Default = 30;
        }

        public static class Reminder
        {
            public const int ReminderDays = 10;
            public const int ReminderDueDays = 8;
        }

        public static class Demand
        {
            public const int DemandDays = 1;
            public const int DemandDueDays = 8;
        }

        public const decimal MinToConsiderPaid = 20m;

        public const int ReferenceRateMarkup = 8;
    }
}
