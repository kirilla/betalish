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

        var terms = new Domain.Entities.PaymentTerms()
        {
            Name = model.Name!,
            ClientId = userToken.ClientId!.Value,
            Interest = model.Interest,
            Reminder = model.Reminder,
            Demand = model.Demand,
            Collect = model.Collect,
            PaymentTermDays = model.PaymentTermDays!.Value,
            MinToConsiderPaid = minToConsiderPaid,
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
