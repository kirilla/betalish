using System.Text.Json;

namespace Betalish.Web.Pages.Admin.Settings;

public class ShowConfigModel(
    IUserToken userToken,
    IDatabaseService database,
    
    IOptions<BadSignInConfiguration> badSignOptions,
    IOptions<FirewallConfiguration> firewallOptions,
    IOptions<SignInConfiguration> signinOptions,
    IOptions<SignUpConfiguration> signupOptions,
    IOptions<SmtpConfiguration> smtpOptions) : AdminPageModel(userToken)
{
    private readonly BadSignInConfiguration _badSignConfig = badSignOptions.Value;
    private readonly FirewallConfiguration _firewallConfig = firewallOptions.Value;
    private readonly SignInConfiguration _signInConfig = signinOptions.Value;
    private readonly SignUpConfiguration _signupConfig = signupOptions.Value;
    private readonly SmtpConfiguration _smtpConfig = smtpOptions.Value;

    public string BadSignInJson { get; set; }
    public string FirewallJson { get; set; }
    public string SignInJson { get; set; }
    public string SignUpJson { get; set; }
    public string SmtpJson { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            BadSignInJson = JsonSerializer.Serialize(
                _badSignConfig, new JsonSerializerOptions { WriteIndented = true });

            FirewallJson = JsonSerializer.Serialize(
                _firewallConfig, new JsonSerializerOptions { WriteIndented = true });

            SignInJson = JsonSerializer.Serialize(
                _signInConfig, new JsonSerializerOptions { WriteIndented = true });

            SignUpJson = JsonSerializer.Serialize(
                _signupConfig, new JsonSerializerOptions { WriteIndented = true });

            SmtpJson = JsonSerializer.Serialize(
                _smtpConfig, new JsonSerializerOptions { WriteIndented = true });

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
