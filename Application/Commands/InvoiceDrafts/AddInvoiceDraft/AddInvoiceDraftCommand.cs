using Betalish.Application.Routines.UpdateInvoiceDraftSummary;

namespace Betalish.Application.Commands.InvoiceDrafts.AddInvoiceDraft;

public class AddInvoiceDraftCommand(
    IDatabaseService database,
    IUpdateInvoiceDraftSummaryRoutine updateSummaryRoutine) : IAddInvoiceDraftCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        InvoiceDraft.AssertInvoiceKindAllowed(model.InvoiceKind!.Value);

        var customer = await database.Customers
            .AsNoTracking()
            .Where(x =>
                x.Id == model.CustomerId!.Value &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var terms = await database.PaymentTerms
            .AsNoTracking()
            .Where(x =>
                x.Id == model.PaymentTermsId!.Value &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var draft = new InvoiceDraft()
        {
            InvoiceKind = model.InvoiceKind!.Value,

            About = model.About!,

            // Dates
            InvoiceDate = null,

            // Customer identity
            Customer_Name = customer.Name,
            CustomerKind = customer.CustomerKind,
            Customer_Ssn10 = customer.Ssn10,
            Customer_Orgnum = customer.Orgnum,

            // Customer address
            Customer_Address1 = customer.Address1,
            Customer_Address2 = customer.Address2,
            Customer_ZipCode = customer.ZipCode,
            Customer_City = customer.City,
            Customer_Country = customer.Country,

            // Customer email
            Customer_Email = customer.EmailAddress,

            // Hints
            CustomerId_Hint = customer.Id,
            CustomerGuid = customer.Guid,

            // Relations
            ClientId = userToken.ClientId!.Value,
            PaymentTermsId = terms?.Id,
        };

        database.InvoiceDrafts.Add(draft);

        if (model.InvoiceTemplateId.HasValue)
        {
            var template = await database.InvoiceTemplates
                .AsNoTracking()
                .Where(x =>
                    x.ClientId == userToken.ClientId!.Value &&
                    x.Id == model.InvoiceTemplateId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var templateRows = await database.InvoiceTemplateRows
                .AsNoTracking()
                .Include(x => x.Article)
                .Where(x =>
                    x.InvoiceTemplate.ClientId == userToken.ClientId!.Value &&
                    x.InvoiceTemplateId == model.InvoiceTemplateId!.Value)
                .ToListAsync();

            var draftRows = templateRows
                .Select(x => new InvoiceDraftRow()
                {
                    InvoiceDraft = draft,

                    IsCredit = false,

                    ArticleNumber = x.Article.Number,
                    ArticleName = x.Article.Name,
                    UnitPrice = x.Article.UnitPrice,
                    Unit = x.Article.UnitName,
                    VatRate = x.Article.VatRate,

                    Quantity = x.Quantity,

                    NetAmount = 0,
                    VatAmount = 0,
                    TotalAmount = 0,

                    RevenueAccount = x.Article.RevenueAccount,
                    VatAccount = x.Article.VatAccount,
                })
                .ToList();

            database.InvoiceDraftRows.AddRange(draftRows);
        }

        await database.SaveAsync(userToken);

        await updateSummaryRoutine.Execute(userToken, draft.Id);

        return draft.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
