using Betalish.Application.Commands.BillingStrategies.EditBillingStrategy;

namespace Betalish.Web.Pages.Clients.BillingStrategies;

public class EditBillingStrategyModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditBillingStrategyCommand command) : ClientPageModel(userToken)
{
    public PaymentTerms BillingStrategy { get; set; } = null!;

    [BindProperty]
    public EditBillingStrategyCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BillingStrategy = await database.PaymentTerms
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditBillingStrategyCommandModel()
            {
                Id = BillingStrategy.Id,
                Name = BillingStrategy.Name,
                Interest = BillingStrategy.Interest,
                Reminder = BillingStrategy.Reminder,
                Demand = BillingStrategy.Demand,
                Collect = BillingStrategy.Collect,
                PaymentTermDays = BillingStrategy.PaymentTermDays,
                MinToConsiderPaid = BillingStrategy.MinToConsiderPaid?.ToSwedish(),
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            BillingStrategy = await database.PaymentTerms
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-billing-strategy/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annan strategi med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
