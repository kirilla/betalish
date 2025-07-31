using Betalish.Application.Commands.InvoiceDrafts.ApproveInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class ApproveInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IApproveInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    [BindProperty]
    public ApproveInvoiceDraftCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new ApproveInvoiceDraftCommandModel()
            {
                Id = InvoiceDraft.Id,
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

            InvoiceDraft = await database.InvoiceDrafts
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            var invoiceId = await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice/{invoiceId}");
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
        catch (RoutineException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Id),
                $"Något gick snett i en inre rutin. " +
                $"Be administratören att kolla loggarna.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
