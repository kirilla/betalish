namespace Betalish.Application.Commands.InvoiceTemplates.RemoveInvoiceTemplate;

public class RemoveInvoiceTemplateCommand(IDatabaseService database) : IRemoveInvoiceTemplateCommand
{
    public async Task Execute(
        IUserToken userToken, RemoveInvoiceTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        if (!model.Confirmed)
            throw new ConfirmationRequiredException();

        var template = await database.InvoiceTemplates
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        database.InvoiceTemplates.Remove(template);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
