using Betalish.Application.Commands.PaymentTerms.AddPaymentTerms;

namespace Betalish.Web.Pages.Clients.PaymentTerms;

public class AddPaymentTermsModel(
    IUserToken userToken,
    IAddPaymentTermsCommand command) : ClientPageModel(userToken)
{
    [BindProperty]
    public AddPaymentTermsCommandModel CommandModel { get; set; } = new();

    public IActionResult OnGet()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            CommandModel = new AddPaymentTermsCommandModel()
            {
                Interest = true,
                Reminder = true,
                Demand = true,
                Collect = false,
                PaymentTermDays = Defaults.Invoice.PaymentTermDays.Default,
                MinToConsiderPaid = Defaults.Invoice.MinToConsiderPaid.ToSwedish(),
                ReminderFee = Defaults.Fee.Reminder.Default.ToSwedish(),
                DemandFee = Defaults.Fee.Demand.Default.ToSwedish(),
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

            return Redirect($"/show-payment-terms/{id}");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns redan betalvillkor med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
