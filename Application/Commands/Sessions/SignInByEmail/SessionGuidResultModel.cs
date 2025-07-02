namespace Betalish.Application.Commands.Sessions.SignInByEmail;

public class SessionGuidResultModel
{
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    public Guid SessionGuid { get; set; }
}
