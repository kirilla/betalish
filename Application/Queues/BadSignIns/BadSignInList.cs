using Betalish.Common.Settings;
using Microsoft.Extensions.Options;
using System.Net;

namespace Betalish.Application.Queues.BadSignIns;

public class BadSignInList(
    IDateService dateService,
    IOptions<BadSignInConfiguration> options) : IBadSignInList
{
    private readonly BadSignInConfiguration _config = options.Value;

    private List<BadSignIn> list { get; set; } = [];

    public void AddSignIn(
        IPAddress? ipAddress,
        string? name,
        string? password,
        SignInBy? signInBy,
        Exception? exception)
    {
        if (!_config.LoggingEnabled)
            return;

        var ip = ipAddress?.ToString();

        //if (ip == "xxx.xxx.xxx.xxx")
        //    return;

        lock (this)
        {
            var signin = new BadSignIn()
            {
                IpAddress = ip,
                Name = null,
                NameLength = name?.Length,
                Password = null,
                PasswordLength = password?.Length,
                Created = dateService.GetDateTimeNow(),
                BadUsername = exception is UserNotFoundException,
                BadPassword = exception is PasswordVerificationFailedException,
                OtherException = 
                    exception is not UserNotFoundException &&
                    exception is not PasswordVerificationFailedException,
                SignInBy = signInBy,
            };

            if (_config.LogUsername)
                signin.Name = name;

            if (_config.LogPassword)
                signin.Password = password;

            list.Add(signin);
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
