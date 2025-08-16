using Betalish.Application.Commands.PaymentTerms.EditPaymentTerms;

namespace Betalish.Web.Pages.Clients.PaymentTerms;

public class EditPaymentTermsModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditPaymentTermsCommand command) : ClientPageModel(userToken)
{
    public Domain.Entities.PaymentTerms PaymentTerms { get; set; } = null!;

    [BindProperty]
    public EditPaymentTermsCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            PaymentTerms = await database.PaymentTerms
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditPaymentTermsCommandModel()
            {
                Id = PaymentTerms.Id,
                Name = PaymentTerms.Name,
                InvoiceKind = PaymentTerms.InvoiceKind,
                Interest = PaymentTerms.Interest,
                Reminder = PaymentTerms.Reminder,
                Demand = PaymentTerms.Demand,
                Collect = PaymentTerms.Collect,
                PaymentTermDays = PaymentTerms.PaymentTermDays,
                MinToConsiderPaid = PaymentTerms.MinToConsiderPaid?.ToSwedish(),
                ReminderFee = PaymentTerms.ReminderFee?.ToSwedish(),
                DemandFee = PaymentTerms.DemandFee?.ToSwedish(),
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

            PaymentTerms = await database.PaymentTerms
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-payment-terms/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns andra betalvillkor med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
