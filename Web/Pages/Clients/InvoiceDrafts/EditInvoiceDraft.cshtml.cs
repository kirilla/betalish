using Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class EditInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public List<BillingStrategy> BillingStrategies { get; set; } = [];

    [BindProperty]
    public EditInvoiceDraftCommandModel CommandModel { get; set; } = new();

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

            BillingStrategies = await database.BillingStrategies
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            CommandModel = new EditInvoiceDraftCommandModel()
            {
                Id = InvoiceDraft.Id,
                About = InvoiceDraft.About,

                // Dates
                InvoiceDate = InvoiceDraft.InvoiceDate.ToIso8601(),

                // Customer address
                Customer_Address1 = InvoiceDraft.Customer_Address1,
                Customer_Address2 = InvoiceDraft.Customer_Address2,
                Customer_ZipCode = InvoiceDraft.Customer_ZipCode,
                Customer_City = InvoiceDraft.Customer_City,
                Customer_Country = InvoiceDraft.Customer_Country,

                // Customer email
                Customer_Email = InvoiceDraft.Customer_Email,

                // Strategy
                IsDebit = InvoiceDraft.IsDebit,
                BillingStrategyId = InvoiceDraft.BillingStrategyId,
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

            BillingStrategies = await database.BillingStrategies
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{id}");
        }
        catch (MissingBillingStrategyException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.BillingStrategyId),
                "Strategi måste anges.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
