namespace Betalish.Application.Commands.PaymentTerms.EditPaymentTerms;

public interface IEditPaymentTermsCommand
{
    Task Execute(IUserToken userToken, EditPaymentTermsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
