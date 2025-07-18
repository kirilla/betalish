namespace Betalish.Application.Commands.InvoiceTemplates.AddInvoiceTemplate;

public class AddInvoiceTemplateCommand(IDatabaseService database) : IAddInvoiceTemplateCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInvoiceTemplateCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var template = new InvoiceTemplate()
        {
            ClientId = userToken.ClientId!.Value,
            Name = model.Name!,
        };

        database.InvoiceTemplates.Add(template);

        await database.SaveAsync(userToken);

        return template.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
