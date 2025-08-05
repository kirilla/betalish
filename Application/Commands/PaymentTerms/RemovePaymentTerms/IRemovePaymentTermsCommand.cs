namespace Betalish.Application.Commands.PaymentTerms.RemovePaymentTerms;

public interface IRemovePaymentTermsCommand
{
    Task Execute(IUserToken userToken, RemovePaymentTermsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
