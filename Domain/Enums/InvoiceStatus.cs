namespace Betalish.Domain.Enums;

public enum InvoiceStatus
{
    Draft = 0,
    Issued = 1,
    Cancelled = 2,

    // NOTE:
    //
    // 'Draft' does not need the invoice number to be set.
    // (This is useful in batch operations.)
    //
    // 'Issued' requires the InvoiceNumber to be set.
    // 
    // 'Cancelled' == "Makulerad".
    //
    // An issued invoice can not be removed,
    // as holes in the invoice number series are not allowed.
    //
    // If, for some reason, an invoice has to be removed,
    // without simply being credited, it must be cancelled.
    // 
    // The common case is to issue a credit invoice, and have
    // it cancel out the debit invoice. This needs no
    // status change, since the debit invoice is still
    // in the 'issued' state.
    // 
    // 'Issued' is the normal and final state for most invoices.
}
