using Betalish.Application.Routines.UpdateInvoiceDraftSummary;

namespace Betalish.Application.Commands.Invoices.CreditInvoiceDraft;

public class CreditInvoiceDraftCommand(
    IDatabaseService database,
    IUpdateInvoiceDraftSummaryRoutine updateSummaryRoutine) : ICreditInvoiceDraftCommand
{
    public async Task<int> Execute(
        IUserToken userToken, CreditInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var invoice = await database.Invoices
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value &&
                x.IsCredit == false)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var invoiceRows = await database.InvoiceRows
            .AsNoTracking()
            .Where(x =>
                x.InvoiceId == invoice.Id &&
                x.Invoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        var balanceRows = await database.BalanceRows
            .AsNoTracking()
            .Where(x =>
                (x.DebitInvoiceId == invoice.Id &&
                x.DebitInvoice.ClientId == userToken.ClientId!.Value) ||
                (x.CreditInvoiceId == invoice.Id &&
                x.CreditInvoice.ClientId == userToken.ClientId!.Value))
            // TODO: Figure out which is which.
            .ToListAsync();

        var draftBalanceRows = await database.DraftBalanceRows
            .AsNoTracking()
            .Where(x =>
                x.InvoiceId == invoice.Id &&
                x.Invoice.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        Assert(
            userToken, 
            invoice, 
            invoiceRows, 
            balanceRows, 
            draftBalanceRows);

        var draft = new InvoiceDraft()
        {
            IsCredit = true,

            About = $"Kreditering av faktura {invoice.InvoiceNumber}.",

            InvoiceDate = null,

            PaymentTermDays = invoice.PaymentTermDays,
            PaymentTerms = invoice.PaymentTerms,

            NetAmount = 0,
            VatAmount = 0,
            Total = 0,
            TotalRounding = 0,

            Customer_Name = invoice.Customer_Name,
            CustomerKind = invoice.CustomerKind,
            Customer_Ssn10 = invoice.Customer_Ssn10,
            Customer_Orgnum = invoice.Customer_Orgnum,

            Customer_Address1 = invoice.Customer_Address1,
            Customer_Address2 = invoice.Customer_Address2,
            Customer_ZipCode = invoice.Customer_ZipCode,
            Customer_City = invoice.Customer_City,
            Customer_Country = invoice.Customer_Country,

            Customer_Email = invoice.Customer_Email,

            CustomerId_Hint = invoice.CustomerId_Hint,
            CustomerGuid = invoice.CustomerGuid,

            ClientId = invoice.ClientId,
        };

        database.InvoiceDrafts.Add(draft);

        var draftRows = invoiceRows
            .Select(x => new InvoiceDraftRow()
            {
                IsCredit = true,

                ArticleNumber = x.ArticleNumber,
                ArticleName = x.ArticleName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Unit = x.Unit,
                VatRate = x.VatRate,

                NetAmount = x.NetAmount,
                VatAmount = x.VatAmount,
                TotalAmount = x.TotalAmount,

                RevenueAccount = x.RevenueAccount,
                VatAccount = x.VatAccount,

                InvoiceDraft = draft,
            })
            .ToList();

        database.InvoiceDraftRows.AddRange(draftRows);

        var draftBalanceRow = new DraftBalanceRow()
        {
            InvoiceNumber = invoice.InvoiceNumber!.Value,
            Amount = invoice.Total,
            Invoice = invoice,
            InvoiceDraft = draft,
        };

        database.DraftBalanceRows.Add(draftBalanceRow);

        await database.SaveAsync(userToken);

        await updateSummaryRoutine.Execute(userToken, draft.Id);

        return draft.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }

    private void Assert(
        IUserToken userToken,
        Invoice invoice,
        List<InvoiceRow> invoiceRows,
        List<BalanceRow> balanceRows,
        List<DraftBalanceRow> draftBalanceRows)
    {
        AssertIsDebetInvoice(invoice);
        AssertHasNoOpenDrafts(draftBalanceRows);
        AssertAmountLeftToCredit(invoice, balanceRows);
    }

    private void AssertIsDebetInvoice(Invoice invoice)
    {
        if (invoice.IsCredit)
            throw new UserFeedbackException(
                "Kreditfakturor kan inte krediteras.");
    }

    private void AssertHasNoOpenDrafts(
        List<DraftBalanceRow> draftBalanceRows)
    {
        if (draftBalanceRows.Count > 0)
            throw new UserFeedbackException(
                "Det finns redan ett utkast för kreditering.");
    }

    private void AssertAmountLeftToCredit(
        Invoice invoice,
        List<BalanceRow> balanceRows)
    {
        decimal creditedAmount = balanceRows.Sum(x => x.Amount);

        decimal leftToCredit = invoice.Total - creditedAmount;

        if (leftToCredit <= 0)
            throw new UserFeedbackException(
                "Inget belopp kvar att kreditera.");
    }
}
