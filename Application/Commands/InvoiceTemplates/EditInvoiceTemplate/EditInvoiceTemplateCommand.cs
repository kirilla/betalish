namespace Betalish.Application.Commands.InvoiceTemplates.EditInvoiceTemplate;

public class EditInvoiceTemplateCommand(IDatabaseService database) : IEditInvoiceTemplateCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var template = await database.InvoiceTemplates
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        template.Name = model.Name!;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
