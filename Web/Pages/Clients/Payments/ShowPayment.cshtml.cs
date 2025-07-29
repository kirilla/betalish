namespace Betalish.Web.Pages.Clients.Payments;

public class ShowPaymentModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public Payment Payment { get; set; } = null!;
    public Invoice? Invoice { get; set; } = null!;

    public List<PaymentAccountingRow> PaymentAccountingRows { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            AssertIsClient();

            Payment = await database.Payments
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            Invoice = await database.Invoices
                .Where(x =>
                    x.Id == Payment.InvoiceId &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync();

            PaymentAccountingRows = await database.PaymentAccountingRows
                .AsNoTracking()
                .Where(x =>
                    x.PaymentId == Payment.Id &&
                    x.Payment.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Account)
                .ToListAsync();

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
}
