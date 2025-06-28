using Betalish.Application.Commands.Signups.SignupForService;
using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Signups;

[AllowAnonymous]
public class SignupForServiceModel(
    IUserToken userToken,
    IDatabaseService database,
    ISignupForServiceCommand command,
    IOptions<AccountConfiguration> options) : UserTokenPageModel(userToken)
{
    private readonly AccountConfiguration _config = options.Value;

    [BindProperty]
    public SignupForServiceCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_config.SignupForServiceAllowed)
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
            if (!_config.SignupForServiceAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new PleaseLogOutException();

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
        catch (Exception ex)
        {
            return Redirect("/help/notpermitted");
        }
    }
}
