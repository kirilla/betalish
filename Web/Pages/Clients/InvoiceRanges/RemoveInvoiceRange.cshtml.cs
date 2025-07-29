using Betalish.Application.Commands.InvoiceRanges.RemoveInvoiceRange;

namespace Betalish.Web.Pages.Clients.InvoiceRanges;

public class RemoveInvoiceRangeModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInvoiceRangeCommand command) : ClientPageModel(userToken)
{
    public InvoiceRange InvoiceRange { get; set; } = null!;

    [BindProperty]
    public RemoveInvoiceRangeCommandModel CommandModel { get; set; } = new();

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

            CommandModel = new RemoveInvoiceRangeCommandModel()
            {
                Id = InvoiceRange.Id,
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

            return Redirect($"/show-invoice-ranges");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
