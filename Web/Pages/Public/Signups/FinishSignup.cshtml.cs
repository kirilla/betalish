using Betalish.Application.Commands.Signups.FinishSignup;
using Microsoft.AspNetCore.Authorization;

namespace Betalish.Web.Pages.Public.Signups;

[AllowAnonymous]
public class FinishSignupModel(
    IUserToken userToken,
    IDatabaseService database,
    IFinishSignupCommand command,
    IOptions<AccountConfiguration> userAccountOptions) : UserTokenPageModel(userToken)
{
    private readonly AccountConfiguration _config = userAccountOptions.Value;

    public Signup Signup { get; set; }

    [BindProperty]
    public FinishSignupCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid guid)
    {
        try
        {
            if (guid == Guid.Empty)
                throw new NotPermittedException();

            if (!_config.FinishSignupAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new PleaseLogOutException();

            Signup = await database.Signups
                .AsNoTracking()
                .Where(x => x.Guid == guid)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new FinishSignupCommandModel()
            {
                Guid = Signup.Guid,
            };

            return Page();
        }
        catch (BlockedByExistingException)
        {
            return Redirect("/signup/blocked");
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (NotFoundException)
        {
            return Redirect("/signup-lost");
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

    public async Task<IActionResult> OnPostAsync(Guid guid)
    {
        try
        {
            if (!_config.FinishSignupAllowed)
                throw new FeatureTurnedOffException();

            if (UserToken.IsAuthenticated)
                throw new PleaseLogOutException();

            Signup = await database.Signups
                .AsNoTracking()
                .Where(x => x.Guid == guid)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/finish-signup-success");
        }
        catch (BlockedByExistingException)
        {
            return Redirect("/signup/blocked");
        }
        catch (BlockedByEmailException)
        {
            return Redirect("/signup/blocked");
        }
        catch (BlockedByGuidException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Guid),
                "Okänt fel. Försök igen.");

            return Page();
        }
        catch (BlockedBySsnException)
        {
            return Redirect("/signup/blocked");
        }
        catch (FeatureTurnedOffException)
        {
            return Redirect("/help/featureturnedoff");
        }
        catch (NotFoundException)
        {
            return Redirect("/signup-lost");
        }
        catch (PleaseLogOutException)
        {
            return Redirect("/help/please-log-out");
        }
        catch (InvalidDataException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Guid),
                "Okänt fel.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
