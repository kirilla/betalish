namespace Betalish.Application.Reports.InvoiceReports;

public class SummedInvoiceAccountingRow
{
    public required string Account { get; set; }

    public required decimal Debit { get; set; }
    public required decimal Credit { get; set; }

    public string Description { get; set; }

    public void Normalize()
    {
        (Debit, Credit) = AccountingLogic.Normalize(Debit, Credit);
    }

    public bool IsEmpty()
    {
        return 
            Debit == 0 && 
            Credit == 0;
    }
}
