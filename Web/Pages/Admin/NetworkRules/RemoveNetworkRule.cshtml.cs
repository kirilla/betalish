using Betalish.Application.Commands.NetworkRules.RemoveNetworkRule;

namespace Betalish.Web.Pages.Admin.NetworkRules;

public class RemoveNetworkRuleModel(
    INetworkRuleCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IRemoveNetworkRuleCommand command) : AdminPageModel(userToken)
{
    public NetworkRule NetworkRule { get; set; } = null!;

    [BindProperty]
    public RemoveNetworkRuleCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            NetworkRule = await database.NetworkRules
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveNetworkRuleCommandModel()
            {
                Id = NetworkRule.Id,
            };

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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            NetworkRule = await database.NetworkRules
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            cacheService.InvalidateCache();

            return Redirect("/show-network-rules");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort adressen.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
