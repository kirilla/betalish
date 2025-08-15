namespace Betalish.Application.Routines.SendDemandEmail;

public interface ISendDemandEmailRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
