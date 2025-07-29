using Betalish.Application.Commands.InvoiceRanges.AddInvoiceRange;

namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class AddInvoiceRangeModel(
    IUserToken userToken,
    IDatabaseService database,
    IAddInvoiceRangeCommand command) : ClientPageModel(userToken)
{
    public List<InvoiceRange> InvoiceRanges { get; set; } = [];

    [BindProperty]
    public AddInvoiceRangeCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceRanges = await database.InvoiceRanges
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.EffectiveStartDate)
                .ThenBy(x => x.EffectiveEndDate)
                .ToListAsync();

            CommandModel = new AddInvoiceRangeCommandModel()
            {
                StartNumber = Ranges.Invoice.Number.Min,
                EndNumber = Ranges.Invoice.Number.Max,
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
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceRanges = await database.InvoiceRanges
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.EffectiveStartDate)
                .ThenBy(x => x.EffectiveEndDate)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-ranges");
        }
        catch (BlockedByDateException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.StartDate),
                "Datumen krockar med en annan nummerserie.");

            return Page();
        }
        catch (BlockedByNumberException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.StartNumber),
                "Numren krockar med en annan nummerserie.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
