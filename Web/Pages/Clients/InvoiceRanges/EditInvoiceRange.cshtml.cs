using Betalish.Application.Commands.InvoiceRanges.EditInvoiceRange;

namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class EditInvoiceRangeModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceRangeCommand command) : ClientPageModel(userToken)
{
    public InvoiceRange InvoiceRange { get; set; } = null!;

    [BindProperty]
    public EditInvoiceRangeCommandModel CommandModel { get; set; }
        = new EditInvoiceRangeCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceRange = await database.InvoiceRanges
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditInvoiceRangeCommandModel()
            {
                Id = InvoiceRange.Id,
                StartNumber = InvoiceRange.StartNumber,
                EndNumber = InvoiceRange.EndNumber,
                Comment = InvoiceRange.Comment,
                StartDate = InvoiceRange.StartDate?.ToIso8601(),
                EndDate = InvoiceRange.EndDate?.ToIso8601(),
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

            InvoiceRange = await database.InvoiceRanges
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

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
