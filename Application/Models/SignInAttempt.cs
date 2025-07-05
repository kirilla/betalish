namespace Betalish.Application.Models;

public class SignInAttempt
{
    public string? Username { get; set; }
    public Endpoint Endpoint { get; set; }
    public DateTime DateTime { get; set; }
}
