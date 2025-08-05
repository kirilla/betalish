namespace Betalish.Application.Commands.PaymentTerms.RemovePaymentTerms;

public class RemovePaymentTermsCommand(IDatabaseService database) : IRemovePaymentTermsCommand
{
    public async Task Execute(
        IUserToken userToken, RemovePaymentTermsCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var terms = await database.PaymentTerms
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.PaymentTerms.Remove(terms);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
