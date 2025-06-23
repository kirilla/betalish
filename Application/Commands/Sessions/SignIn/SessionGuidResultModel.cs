namespace Betalish.Application.Commands.Sessions.SignIn;

public class SessionGuidResultModel
{
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    public Guid SessionGuid { get; set; }
}
