using System.Security.Principal;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Betalish.Application.Commands.Articles.EditArticle;

public class EditArticleCommand(IDatabaseService database) : IEditArticleCommand
{
    public async Task Execute(
        IUserToken userToken, EditArticleCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        if (await database.Articles
            .AnyAsync(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Number == model.Number!.Value &&
                x.Id != model.Id))
            throw new BlockedByNumberException();

        var article = await database.Articles
            .Where(x =>
                x.Id == model.Id &&
                x.ClientId == userToken.ClientId!.Value)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        article.ArticleKind = model.ArticleKind!.Value;
        article.Number = model.Number!.Value;
        article.Name = model.Name!;
        article.UnitPrice = model.UnitPrice!.TryParseDecimal()!.Value;
        article.VatValue = model.VatValue!.TryParseDecimal()!.Value;
        article.UnitName = model.UnitName!;
        article.Account = model.Account!;
        article.VatAccount = model.VatAccount!;
        article.ClientId = userToken.ClientId!.Value;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
