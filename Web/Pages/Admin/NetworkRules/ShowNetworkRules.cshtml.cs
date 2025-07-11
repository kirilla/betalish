namespace Betalish.Web.Pages.Admin.NetworkRules;

public class ShowNetworkRulesModel(
    IUserToken userToken,
    IDatabaseService database,
    IOptions<FirewallConfiguration> firewallOptions) : AdminPageModel(userToken)
{
    public readonly FirewallConfiguration IpFilterConfiguration = firewallOptions.Value;

    public List<NetworkRule> NetworkRules { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            AssertIsAdmin();

            NetworkRules = await database.NetworkRules.ToListAsync();

            NetworkRules = NetworkRules
                .OrderByDescending(x => x.PrefixLength) // Higher specificity
                .ThenBy(x => x.BaseAddress)
                .ToList();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
