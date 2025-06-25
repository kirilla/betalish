using System.Net;

namespace Betalish.Application.Queues.BadSignIns;

public class BadSignInList(IDateService dateService) : IBadSignInList
{
    private List<BadSignIn> list { get; set; } = new List<BadSignIn>();

    public void AddSignIn(
        IPAddress? ipAddress,
        string? name,
        string? password,
        Exception? exception)
    {
        var ip = ipAddress?.ToString();

        //if (ip == "xxx.xxx.xxx.xxx")
        //    return;

        lock (this)
        {
            var visit = new BadSignIn()
            {
                IpAddress = ip,
                Name = name,
                Password = password,
                Created = dateService.GetDateTimeNow(),
                BadUsername = exception is UserNotFoundException,
                BadPassword = exception is PasswordVerificationFailedException,
                OtherException = 
                    exception is not UserNotFoundException &&
                    exception is not PasswordVerificationFailedException,
            };

            list.Add(visit);
        }
    }

    public List<BadSignIn> TakeBadSignIns()
    {
        lock (this)
        {
            var entries = list.ToList();

            list.Clear();

            return entries;
        }
    }
}
