namespace Betalish.Application.Commands.PaymentTerms.AddPaymentTerms;

public interface IAddPaymentTermsCommand
{
    Task<int> Execute(IUserToken userToken, AddPaymentTermsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
