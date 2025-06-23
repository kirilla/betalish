using Betalish.Application.Commands.EmailMessages.RemoveEmailMessage;

namespace Betalish.Web.Pages.Admin.EmailMessages;

public class RemoveEmailMessageModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveEmailMessageCommand command) : AdminPageModel(userToken)
{
    public EmailMessage EmailMessage { get; set; }

    [BindProperty]
    public RemoveEmailMessageCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new RemoveEmailMessageCommandModel()
            {
                Id = EmailMessage.Id,
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

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await AssertAdminAuthorization(database);

            if (!await command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Where(x => x.Id == CommandModel.Id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/admin/show-desktop");
        }
        catch (ConfirmationRequiredException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Confirmed),
                "Bekräfta att du verkligen vill ta bort meddelandet.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
