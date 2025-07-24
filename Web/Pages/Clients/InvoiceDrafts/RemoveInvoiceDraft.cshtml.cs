using Betalish.Application.Commands.InvoiceDrafts.RemoveInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class RemoveInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    [BindProperty]
    public RemoveInvoiceDraftCommandModel CommandModel { get; set; }
        = new RemoveInvoiceDraftCommandModel();

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

            CommandModel = new RemoveInvoiceDraftCommandModel()
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

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-drafts");
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
