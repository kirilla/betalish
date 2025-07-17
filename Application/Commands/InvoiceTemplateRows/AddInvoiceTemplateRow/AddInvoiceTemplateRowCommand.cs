namespace Betalish.Application.Commands.InvoiceTemplateRows.AddInvoiceTemplateRow;

public class AddInvoiceTemplateRowCommand(IDatabaseService database) : IAddInvoiceTemplateRowCommand
{
    public async Task Execute(
        IUserToken userToken, AddInvoiceTemplateRowCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var template = await database.InvoiceTemplates
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.InvoiceTemplateId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var article = await database.Articles
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.ArticleId)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        if (await database.InvoiceTemplateRows
            .AnyAsync(x =>
                x.InvoiceTemplate.ClientId == userToken.ClientId!.Value &&
                x.InvoiceTemplateId == model.InvoiceTemplateId &&
                x.ArticleId == model.ArticleId!.Value))
            throw new BlockedByExistingException();

        var row = new InvoiceTemplateRow()
        {
            ArticleId = article.Id,
            InvoiceTemplateId = template.Id,
            Quantity = model.Quantity!.TryParseDecimal()!.Value,
        };

        database.InvoiceTemplateRows.Add(row);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
