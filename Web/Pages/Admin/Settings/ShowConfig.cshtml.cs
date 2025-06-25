namespace Betalish.Web.Pages.Admin.Settings;

public class ShowConfigModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<AccountConfiguration> accountOptions,
    IOptions<BadSignInConfiguration> badSignOptions,
    IOptions<FirewallConfiguration> firewallOptions,
    IOptions<SmtpConfiguration> smtpOptions) : AdminPageModel(userToken)
{
    public readonly AccountConfiguration _accountConfig = accountOptions.Value;
    public readonly BadSignInConfiguration _badSignConfig = badSignOptions.Value;
    public readonly FirewallConfiguration _firewallConfig = firewallOptions.Value;
    public readonly SmtpConfiguration _smtpConfig = smtpOptions.Value;

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
