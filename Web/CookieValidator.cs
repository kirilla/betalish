using Betalish.Application.Queues.SessionActivities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Betalish.Web;

public class CookieValidator(
    IDatabaseService database,
    IHttpContextAccessor httpContextAccessor,
    ISessionActivityList sessionActivityList) : CookieAuthenticationEvents
{
    public override async Task ValidatePrincipal(
        CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

        var userGuid = userPrincipal?.Claims
            .Where(x => x.Type == "UserGuid")
            .Select(x => x.Value)
            .FirstOrDefault();

        var sessionGuid = userPrincipal?.Claims
            .Where(x => x.Type == "SessionGuid")
            .Select(x => x.Value)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(userGuid) ||
            string.IsNullOrEmpty(sessionGuid))
        {
            context.RejectPrincipal();

            await context.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return;
        }

        var session = database.Sessions
            .Where(x =>
                x.Guid.ToString() == sessionGuid &&
                x.User.Guid.ToString() == userGuid)
            .Select(x => new
            {
                SessionId = x.Id,
                x.UserId,
                x.User.Name,
                x.ClientId,
                ClientName = x.Client.Name,
            })
            .SingleOrDefault();

        if (session == null)
        {
            context.RejectPrincipal();

            await context.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return;
        }

        var items = httpContextAccessor.HttpContext?.Items;

        items!["UserGuid"] = userGuid;
        items!["SessionGuid"] = sessionGuid;

        items!["UserId"] = session.UserId;
        items!["SessionId"] = session.SessionId;

        items!["UserName"] = session.Name;

        items!["ClientId"] = session.ClientId;
        items!["ClientName"] = session.ClientName;

        sessionActivityList.AddSessionId(session.SessionId);
    }
}
