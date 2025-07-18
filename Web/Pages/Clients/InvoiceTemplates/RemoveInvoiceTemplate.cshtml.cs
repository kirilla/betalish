using Betalish.Application.Commands.InvoiceTemplates.RemoveInvoiceTemplate;

namespace Betalish.Web.Pages.Clients.InvoiceTemplates;

public class RemoveInvoiceTemplateModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveInvoiceTemplateCommand command) : ClientPageModel(userToken)
{
    public InvoiceTemplate InvoiceTemplate { get; set; } = null!;

    [BindProperty]
    public RemoveInvoiceTemplateCommandModel CommandModel { get; set; }
        = new RemoveInvoiceTemplateCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x => 
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveInvoiceTemplateCommandModel()
            {
                Id = InvoiceTemplate.Id,
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

            InvoiceTemplate = await database.InvoiceTemplates
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-templates");
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
