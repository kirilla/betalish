namespace Betalish.Application.Commands.PaymentTerms.AddPaymentTerms;

public class AddPaymentTermsCommand(IDatabaseService database) : IAddPaymentTermsCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddPaymentTermsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.PaymentTerms
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name))
            throw new BlockedByExistingException();

        var minToConsiderPaid = model.MinToConsiderPaid?.TryParseDecimal();
        var reminderFee = model.ReminderFee?.TryParseDecimal();
        var demandFee = model.DemandFee?.TryParseDecimal();

        Domain.Entities.PaymentTerms.AssertInvoiceKindAllowed(model.InvoiceKind!.Value);

        // TODO: Fee limits

        var terms = new Domain.Entities.PaymentTerms()
        {
            Name = model.Name!,
            InvoiceKind = model.InvoiceKind!.Value,
            ClientId = userToken.ClientId!.Value,
            Interest = model.Interest,
            Reminder = model.Reminder,
            Demand = model.Demand,
            Collect = model.Collect,
            PaymentTermDays = model.PaymentTermDays!.Value,
            MinToConsiderPaid = minToConsiderPaid,
            ReminderFee = reminderFee,
            DemandFee = demandFee,
        };

        database.PaymentTerms.Add(terms);

        await database.SaveAsync(userToken);

        return terms.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
