namespace Betalish.Application.Commands.CustomerTags.EditCustomerTags;

public interface IEditCustomerTagsCommand
{
    Task Execute(IUserToken userToken, EditCustomerTagsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
