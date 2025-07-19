namespace Betalish.Application.Commands.Customers.EditCustomerTags;

public interface IEditCustomerTagsCommand
{
    Task Execute(IUserToken userToken, EditCustomerTagsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
