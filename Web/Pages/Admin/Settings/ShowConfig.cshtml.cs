using System.Text.Json;

namespace Betalish.Web.Pages.Admin.Settings;

public class ShowConfigModel(
    IUserToken userToken,
    IOptions<BadSignInConfiguration> badSignOptions,
    IOptions<DistributionConfiguration> distributionOptions,
    IOptions<FirewallConfiguration> firewallOptions,
    IOptions<SignInConfiguration> signinOptions,
    IOptions<SignUpConfiguration> signupOptions,
    IOptions<SmtpConfiguration> smtpOptions) : AdminPageModel(userToken)
{
    private readonly BadSignInConfiguration _badSignConfig = badSignOptions.Value;
    private readonly DistributionConfiguration _distributionConfig = distributionOptions.Value;
    private readonly FirewallConfiguration _firewallConfig = firewallOptions.Value;
    private readonly SignInConfiguration _signInConfig = signinOptions.Value;
    private readonly SignUpConfiguration _signupConfig = signupOptions.Value;
    private readonly SmtpConfiguration _smtpConfig = smtpOptions.Value;

    public string BadSignInJson { get; set; } = null!;
    public string DistributionJson { get; set; } = null!;
    public string FirewallJson { get; set; } = null!;
    public string SignInJson { get; set; } = null!;
    public string SignUpJson { get; set; } = null!;
    public string SmtpJson { get; set; } = null!;

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            BadSignInJson = JsonSerializer.Serialize(_badSignConfig, options);
            DistributionJson = JsonSerializer.Serialize(_distributionConfig, options);
            FirewallJson = JsonSerializer.Serialize(_firewallConfig, options);
            SignInJson = JsonSerializer.Serialize(_signInConfig, options);
            SignUpJson = JsonSerializer.Serialize(_signupConfig, options);
            SmtpJson = JsonSerializer.Serialize(_smtpConfig, options);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
