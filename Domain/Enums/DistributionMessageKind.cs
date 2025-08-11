namespace Betalish.Domain.Enums;

public enum DistributionMessageKind
{
    InvoiceEmail = 1001,
    InvoicePostal = 1002,

    ReminderEmail = 2001,
    ReminderPostal = 2002,

    DemandEmail = 3001,
    DemandPostal = 3002,

    CollectEmail = 4001,
    CollectPostal = 4002,
}
