using Betalish.Application.Commands.NetworkRules.AddNetworkRule;

namespace Betalish.Web.Pages.Admin.NetworkRules;

public class AddNetworkRuleModel(
    INetworkRuleCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IAddNetworkRuleCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public AddNetworkRuleCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddNetworkRuleCommandModel()
            {
                Active = true,
            };

            return Page();
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
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            cacheService.InvalidateCache();

            return Redirect("/show-network-rules");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.BaseAddress),
                "Intervallet finns redan.");

            return Page();
        }
        catch (FormatException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.PrefixLength),
                "Oväntat format.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
