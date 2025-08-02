namespace Betalish.Domain.Enums;

public enum PlannedItemKind
{
    Invoice = 1,
    Interest = 2, // Dröjsmålsränta
    Reminder = 3, // Påminnelse
    Demand = 4, // Inkassokrav
    Collect = 5, // KFM-ansökan
}

/*
| Steg | Åtgärd                                        | Avgift/Ränta          |
| ---- | --------------------------------------------- | --------------------- |
| 1    | Faktura skickas                               | —                     |
| 2    | Dröjsmålsränta efter 30 dagar (om ej avtalat) | Ref.ränta + 8 %       |
| 3    | Påminnelse (ej krav, god sed 1 st)            | Max 60 kr, om avtalat |
| 4    | Inkassokrav                                   | Max 180 kr            |
| 5    | KFM-ansökan                                   | Ytterligare avgifter  |
 */
