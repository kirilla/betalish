using Betalish.Application.Commands.InvoiceDrafts.EditInvoiceDraft;

namespace Betalish.Web.Pages.Clients.InvoiceDrafts;

public class EditInvoiceDraftModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditInvoiceDraftCommand command) : ClientPageModel(userToken)
{
    public InvoiceDraft InvoiceDraft { get; set; } = null!;

    public List<Domain.Entities.PaymentTerms> PaymentTerms { get; set; } = [];

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

            PaymentTerms = await database.PaymentTerms
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

                // PaymentTerms
                IsDebitLike = InvoiceDraft.IsDebitLike,
                PaymentTermsId = InvoiceDraft.PaymentTermsId,
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

            PaymentTerms = await database.PaymentTerms
                .AsNoTracking()
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-invoice-draft/{id}");
        }
        catch (MissingPaymentTermsException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.PaymentTermsId),
                "Betalvillkor måste anges.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
