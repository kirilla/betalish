namespace Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

public class EditInvoiceDraftCommand(IDatabaseService database) : IEditInvoiceDraftCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceDraftCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (model.Customer_Country.IsMissingValue())
        {
            model.Customer_ZipCode = 
                model.Customer_ZipCode?.StripNonNumeric();
        }

        var draft = await database.InvoiceDrafts
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (draft.IsDebit &&
            draft.BillingStrategyId == null)
            throw new MissingBillingStrategyException();

        draft.About = model.About!;

        // Dates
        draft.InvoiceDate = model.InvoiceDate?.ToIso8601DateOnly();

        // Customer address
        draft.Customer_Address1 = model.Customer_Address1;
        draft.Customer_Address2 = model.Customer_Address2;
        draft.Customer_ZipCode = model.Customer_ZipCode!;
        draft.Customer_City = model.Customer_City!;
        draft.Customer_Country = model.Customer_Country;

        // Customer email
        draft.Customer_Email = model.Customer_Email?.ToLowerInvariant();

        // Strategy
        if (model.BillingStrategyId.HasValue)
        {
            var strategy = await database.PaymentTerms
                .AsNoTracking()
                .Where(x =>
                    x.Id == model.BillingStrategyId!.Value &&
                    x.ClientId == userToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            draft.BillingStrategyId = strategy.Id;
        }

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
