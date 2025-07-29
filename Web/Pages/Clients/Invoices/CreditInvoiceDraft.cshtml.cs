using Betalish.Application.Commands.Invoices.CreditInvoiceDraft;

namespace Betalish.Web.Pages.Clients.Invoices;

public class CreditInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    ICreditInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public Invoice Invoice { get; set; } = null!;

    [BindProperty]
    public CreditInvoiceDraftCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new CreditInvoiceDraftCommandModel()
            {
                Id = Invoice.Id,
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

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var draftId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{draftId}");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill kreditera fakturan.");

            return Page();
        }
        catch (UserFeedbackException ex)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Id),
                ex.Message);

            // Note:
            //
            // We're exposing internals here, taking a calculated risk,
            // to give the user a meaningful error message without 
            // having to add a specific exception type for every assertion.

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
