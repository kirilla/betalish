namespace Betalish.Application.Commands.PaymentTerms.EditPaymentTerms;

public class EditPaymentTermsCommand(IDatabaseService database) : IEditPaymentTermsCommand
{
    public async Task Execute(
        IUserToken userToken, EditPaymentTermsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.PaymentTerms
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Name == model.Name &&
                x.Id != model.Id))
            throw new BlockedByExistingException();

        var minToConsiderPaid = model.MinToConsiderPaid?.TryParseDecimal();
        var reminderFee = model.ReminderFee?.TryParseDecimal();
        var demandFee = model.DemandFee?.TryParseDecimal();

        // TODO: Fee limits

        var terms = await database.PaymentTerms
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        terms.Name = model.Name!;
        terms.Interest = model.Interest;
        terms.Reminder = model.Reminder;
        terms.Demand = model.Demand;
        terms.Collect = model.Collect;
        terms.PaymentTermDays = model.PaymentTermDays!.Value;
        terms.MinToConsiderPaid = minToConsiderPaid;
        terms.ReminderFee = reminderFee;
        terms.DemandFee = demandFee;
        
        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
