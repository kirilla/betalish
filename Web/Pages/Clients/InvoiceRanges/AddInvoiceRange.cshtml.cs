using Betalish.Application.Commands.InvoiceRanges.AddInvoiceRange;

namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class AddInvoiceRangeModel(
    IUserToken userToken,
    IAddInvoiceRangeCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddInvoiceRangeCommandModel CommandModel { get; set; }
        = new AddInvoiceRangeCommandModel();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

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

            if (!ModelState.IsValid)
                return Page();

            var id = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-range/{id}");
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
