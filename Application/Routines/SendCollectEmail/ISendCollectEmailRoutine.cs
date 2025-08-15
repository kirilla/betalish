namespace Betalish.Application.Routines.SendCollectEmail;

public interface ISendCollectEmailRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
