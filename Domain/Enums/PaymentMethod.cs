namespace Betalish.Domain.Enums;

public enum PaymentMethod
{
    Cash = 1,

    Autogiro = 2,
    Bankgiro = 3,
    Plusgiro = 4,

    Swish = 5,

    Iban = 6,

    Depreciation = 7,
    DepreciationVat = 8,
    
    Kvittning = 9,
    
    Move = 10,

    //LossOfCustom = 11,
}
