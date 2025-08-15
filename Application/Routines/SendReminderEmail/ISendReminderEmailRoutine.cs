namespace Betalish.Application.Routines.SendReminderEmail;

public interface ISendReminderEmailRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
