namespace Betalish.Domain.Enums;

public enum PaymentKind
{
    Payment = 1,
    //LbPayment = 2,

    Balance = 3, // Kvittning
    Move = 4, // Flytt

    Depreciation = 5,
    DepreciationVat = 6,
}
