using Betalish.Application.Commands.NetworkRules.EditNetworkRule;

namespace Betalish.Web.Pages.Admin.NetworkRules;

public class EditNetworkRuleModel(
    INetworkRuleCacheService cacheService,
    IUserToken userToken,
    IDatabaseService database,
    IEditNetworkRuleCommand command) : AdminPageModel(userToken)
{
    public NetworkRule NetworkRule { get; set; }

    [BindProperty]
    public EditNetworkRuleCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            NetworkRule = await database.NetworkRules
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditNetworkRuleCommandModel()
            {
                Id = NetworkRule.Id,
                BaseAddress2 = NetworkRule.BaseAddress2,
                Prefix2 = NetworkRule.Prefix2,
                Blocked = NetworkRule.Blocked,
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
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
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
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.BaseAddress2),
                "Intervallet finns redan.");

            return Page();
        }
        catch (FormatException ex)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Prefix2),
                "Ogiltig CIDR-adressblock.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
