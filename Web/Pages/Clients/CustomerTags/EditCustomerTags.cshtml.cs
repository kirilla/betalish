using Betalish.Application.Commands.CustomerTags.EditCustomerTags;

namespace Betalish.Web.Pages.Clients.CustomerTags;

public class EditCustomerTagsModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditCustomerTagsCommand command) : ClientPageModel(userToken)
{
    public Customer Customer { get; set; } = null!;

    [BindProperty]
    public EditCustomerTagsCommandModel CommandModel { get; set; }
        = new EditCustomerTagsCommandModel();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Customer = await database.Customers
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            var tags = await database.CustomerTags
                .Where(x => 
                    x.Customer.ClientId == UserToken.ClientId!.Value &&
                    x.CustomerId == Customer.Id)
                .OrderBy(x => x.Key)
                .ToListAsync();

            var values = tags
                .Select(x =>
                    x.Value == null ?
                    x.Key :
                    $"{x.Key}: {x.Value}")
                .Cast<string>()
                .ToList()
                .Join(", ");

            CommandModel = new EditCustomerTagsCommandModel()
            {
                Id = Customer.Id,
                Tags = values,
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

            Customer = await database.Customers
                .Where(x =>
                    x.Id == id &&
                    x.ClientId == UserToken.ClientId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-customer/{id}");
        }
        catch (TooLongException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Tags),
                $"Någon etikett eller något värde är för långt. " +
                $"Max {MaxLengths.Common.Tag.Key} tecken för etikett, " +
                $"och max {MaxLengths.Common.Tag.Value} för värden.");

            return Page();
        }
        catch (TooManyException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Tags),
                $"Max {Limits.Customer.Tag.Max} etiketter.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
