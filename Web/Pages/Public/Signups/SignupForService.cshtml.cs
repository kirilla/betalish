using Betalish.Application.Commands.Signups.SignupForService;
using Betalish.Application.Models;
using Betalish.Application.Queues.EndpointRateLimiting;
using Betalish.Application.Queues.IpAddressRateLimiting;
using Betalish.Common.Dates;
using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Signups;

[AllowAnonymous]
public class SignupForServiceModel(
    IUserToken userToken,
    IDatabaseService database,
    IDateService dateService,
    IEndpointRateLimiter endpointRateLimiter,
    IIpAddressRateLimiter ipAddressRateLimiter,
    ISignupForServiceCommand command,
    IOptions<SignUpConfiguration> options) : UserTokenPageModel(userToken)
{
    private readonly SignUpConfiguration _config = options.Value;

    [BindProperty]
    public SignupForServiceCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_config.AllowSignupForService)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new PleaseLogOutException();

            return Page();
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (PleaseLogOutException)
        {
            return Redirect("/help/please-log-out");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!_config.AllowSignupForService)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new PleaseLogOutException();

            endpointRateLimiter.TryRateLimit(100, new EndpointHit()
            {
                DateTime = dateService.GetDateTimeNow(),
                Endpoint = Betalish.Common.Enums.Endpoint.SignupForService,
            });

            ipAddressRateLimiter.TryRateLimit(10, new IpAddressEndpointHit()
            {
                DateTime = dateService.GetDateTimeNow(),
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Endpoint = Betalish.Common.Enums.Endpoint.SignupForService,
            });

            await Task.Delay(
                TimeSpan.FromMilliseconds(
                    Random.Shared.Next(200, 600)));

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/signup-success");
        }
        catch (BlockedBySsnException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Ssn12),
                "Det finnas redan ett konto med samma personnummer.");

            return Page();
        }
        catch (BlockedByEmailException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.EmailAddress),
                "Det finnas redan ett konto med samma epostadress.");

            return Page();
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (InvalidSsnException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Ssn12),
                "Ogiltigt personnummer.");

            return Page();
        }
        catch (PleaseLogOutException)
        {
            return Redirect("/help/please-log-out");
        }
        catch (RateLimitedException)
        {
            return Redirect("/help/rate-limited");
        }
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
